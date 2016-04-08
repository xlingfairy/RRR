using RRExpress.ApiClient.Exceptions;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RRExpress.ApiClient {
    public sealed class ApiClient {


        /// <summary>
        /// 是否已初始化
        /// </summary>
        private static bool IsInitilized = false;

        /// <summary>
        /// ApiClient 选项
        /// </summary>
        private static ApiClientOption Option = null;

        /// <summary>
        /// ApiClient 单实例
        /// </summary>
        private static Lazy<ApiClient> Instance = new Lazy<ApiClient>(() => new ApiClient());

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler<ApiClientMessageArgs> OnMessage = null;


        private ApiClient() {

        }



        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="option"></param>
        public static void Init(ApiClientOption option) {
            if (option == null)
                throw new ArgumentNullException("option");

            if (!IsInitilized) {
                IsInitilized = true;

                Option = option;

            } else {
                throw new Exception("ApiClient has been initilized, can't initialize again");
            }
        }






        /// <summary>
        /// 构建方法的请求地址
        /// </summary>
        /// <param name="mth"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string BuildUri(BaseMethod mth, HttpContent content = null) {
            var url = mth.Model.FixUrl(Option.BaseUri);
            if (content == null) {
                return url.SetUrlKeyValue(mth.GetParams().ToDictionary(d => d.Key, d => d.Value.ToString()));
            } else
                return url;
        }

        private async Task<T> InnerExecute<T>(BaseMethod<T> method) {
            if (method == null)
                throw new ArgumentNullException("method");

            //TODO
            //var results = method.Validate();
            //if (!results.IsValid) {
            //    throw new MethodValidationException(results);
            //}

            try {
                return await method.Execute(this);
            } catch (HttpRequestException ex) {
                DealException(method, ErrorTypes.Unknow, ex);
            } catch (ParseException ex) {
                DealException(method, ErrorTypes.ParseError, ex);
            } catch (MethodExecuteException ex) {
                DealException(method, ex.ErrorType, ex);
            } catch (Exception ex) {
                DealException(method, ErrorTypes.Unknow, ex);
            }

            return default(T);
        }


        private void DealException(BaseMethod method, ErrorTypes type, Exception ex, string msg = null) {
            if (OnMessage != null) {
                OnMessage.Invoke(method, new ApiClientMessageArgs() {
                    ErrorType = type,
                    Message = msg ?? ex.GetBaseException().Message
                });
            }
            method.ErrorType = type;
            method.ErrorMsg = msg ?? ex.GetBaseException().Message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        public static async Task<T> Execute<T>(BaseMethod<T> method) {
            return await ApiClient.Instance.Value.InnerExecute(method);
        }
    }
}
