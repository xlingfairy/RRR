using RRExpress.Common;
using RRExpress.Common.Exceptions;
using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace RRExpress.ApiClient {
    public class ApiClient {

        public static event EventHandler<MessageArgs> OnMessage = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static Lazy<ApiClient> Instance = new Lazy<ApiClient>(() => new ApiClient());

        /// <summary>
        /// 选项数据
        /// </summary>
        private static ApiClientOption Option = null;

        /// <summary>
        /// 是否以经初始化
        /// </summary>
        private static bool IsInitilized = false;


        private Dictionary<Type, IClientSetup> SetupDic;


        private IEnumerable<IClientSetup> _setups = null;




        /// <summary>
        /// MEF 导入
        /// </summary>
        [ImportMany]
        private IEnumerable<IClientSetup> Setups {
            get {
                return this._setups;
            }
            set {
                this._setups = value;
                if (value != null && value.Count() > 0) {
                    this.SetupDic = value.ToDictionary(c => c.GetType(), c => c);
                }
                else
                    this.SetupDic = new Dictionary<Type, IClientSetup>();
            }
        }



        [Import]
        public IWebApiBearerTokenProvider TokenProvider {
            get; set;
        }

        [Import]
        public IApiClientCacheProvider CacheProvider { get; set; }

        /// <summary>
        /// 私有构造，不允许直接实例
        /// </summary>
        private ApiClient() {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="option"></param>
        public static void Init(IEnumerable<Assembly> assemblies, ApiClientOption option = null) {
            if (!IsInitilized) {
                IsInitilized = true;

                Option = option ?? ApiClientOption.Default;
                //MEF 注入
                var container = new ContainerConfiguration()
                             //.WithAssembly(typeof(RRExpress.Api.V1.Setup).GetTypeInfo().Assembly)
                             .WithAssemblies(assemblies)
                             .CreateContainer();

                container.SatisfyImports(Instance.Value);
            }
            else {
                //throw new Exception("ApiClient has been initilized, can't initialize again");
            }
        }


        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        /// <exception cref="MethodValidationException"></exception>
        /// <exception cref="NotSupportedException">Method 的 ClientSetupType 对应的类型，没有导出为 IClientSetup </exception>
        public async Task<T> Execute<T>(BaseMethod<T> method) {
            if (method == null)
                throw new ArgumentNullException("method");

            if (!this.SetupDic.ContainsKey(method.ClientSetupType)) {
                throw new NotSupportedException(string.Format("{0} not Export as IClient", method.ClientSetupType.FullName));
            }

            if (!IsInitilized) {
                throw new Exception("ApiClient must Init befor use it.");
            }

            //TODO 参数验证
            //var results = method.Validate();
            //if (!results.IsValid) {
            //    var msg = string.Join(";", results.Select(m => m.Message));
            //    this.DealException(method, ErrorTypes.ParameterError, new MethodValidationException(results), msg);
            //    return default(T);
            //}

            var setup = this.SetupDic[method.ClientSetupType];
            if (!setup.IsValid) {
                //验证配置
                this.DealException(method, ErrorTypes.SetupError, new ClientSetupException(setup));
                return default(T);
            }

            try {
                var data = await method.Execute(Option, setup);
                //如果方法设置允许缓存,则保存到缓存
                if (method.AllowCache) {
                    await this.CacheProvider.Store(method, data);
                }
                return data;
            }
            catch (HttpRequestException ex) {
                this.DealException(method, ErrorTypes.Unknow, ex);
            }
            catch (ParseException ex) {
                this.DealException(method, ErrorTypes.ParseError, ex);
            }
            catch (MethodRequestException ex) {
                this.DealException(method, ex.ErrorType, ex);
            }
            catch (ContentWithErrorException ex) {
                this.DealException(method, ErrorTypes.ResponsedWithErrorInfo, ex);
            }
            catch (NetworkException ex) {
                this.DealException(method, ErrorTypes.Network, ex);
            }
            catch (Exception ex) {
                this.DealException(method, ErrorTypes.Unknow, ex);
            }
            return default(T);
        }

        /// <summary>
        /// 从缓存中取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<T> GetDataFromCache<T>(BaseMethod<T> method) {
            return await this.CacheProvider.Restore<T>(method);
        }

        private void DealException(BaseMethod method, ErrorTypes type, Exception ex, string msg = null) {
            if (OnMessage != null) {
                OnMessage.Invoke(method, new MessageArgs() {
                    ErrorType = type,
                    Message = msg ?? ex.GetBaseException().Message
                });
            }
            method.ErrorType = type;
            method.ErrorMsg = msg ?? ex.GetBaseException().Message;
        }
    }
}
