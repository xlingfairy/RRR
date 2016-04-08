using Newtonsoft.Json;
using RRExpress.ApiClient.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ApiClient {
    public abstract class BaseMethod {

        /// <summary>
        /// Let Json.Net support abstract class serialize & deserialize
        /// </summary>
        private static readonly JsonSerializerSettings Setting =
            new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.Auto
            };

        /// <summary>
        /// Which Api, must be {controller}/{action}
        /// </summary>
        public abstract string Model {
            get;
        }

        /// <summary>
        /// Requst Method
        /// </summary>
        public abstract HttpMethod HttpMethod {
            get;
        }




        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool HasError {
            get {
                return this.ErrorType != null;
            }
        }

        /// <summary>
        /// 错误类型
        /// </summary>
        public ErrorTypes? ErrorType {
            get; set;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg {
            get;
            set;
        }



        protected virtual object GetSendData() {
            //return this.GetParams();
            return null;
        }

        public virtual HttpContent GetContent() {
            var data = this.GetSendData();
            if (data != null) {
                var json = JsonConvert.SerializeObject(data, Setting);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                return content;
            }
            return null;
        }

        ///// <summary>
        ///// 检查输入参数的合法性
        ///// </summary>
        ///// <returns></returns>
        //public ValidationResults Validate() {
        //    var validator = ValidationFactory.CreateValidator(this.GetType());
        //    var results = validator.Validate(this);

        //    var msgs = this.InnerValidate();
        //    results.AddAllResults(msgs.Select(m => new ValidationResult(m, this, "", "", null)));

        //    return results;
        //}

        /// <summary>
        /// 验证数据（虚方法，在子类中重写验证）
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<string> InnerValidate() {
            return Enumerable.Empty<string>();
        }

        protected async virtual Task<Tuple<HttpStatusCode, byte[]>> GetResult(ApiClient client) {
            using (var content = this.GetContent())
            using (var hc = new HttpClient()) {
                var request = new HttpRequestMessage(this.HttpMethod, client.BuildUri(this, content));
                if (content != null) {
                    request.Content = content;
                }

                var rep = await hc.SendAsync(request);
                var bytes = await rep.Content.ReadAsByteArrayAsync();
                return new Tuple<HttpStatusCode, byte[]>(rep.StatusCode, bytes);
            }
        }
    }

    public abstract class BaseMethod<T> : BaseMethod {

        /// <summary>
        /// Parse Data from Api Result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual T Parse(byte[] result) {
            var json = Encoding.UTF8.GetString(result, 0, result.Length);
            return JsonConvert.DeserializeObject<T>(json);
        }


        internal async Task<T> Execute(ApiClient client) {
            var result = await this.GetResult(client);
            var status = result.Item1;

            var error = status.Convert();
            if (error.HasValue) {
                throw new MethodExecuteException() {
                    StateCode = status,
                    ErrorType = error.Value
                };
            }

            try {
                return this.Parse(result.Item2);
            } catch {
                throw new ParseException() {
                    TargetType = typeof(T),
                    TargetData = result.Item2
                };
            }
        }
    }
}
