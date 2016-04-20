using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common {
    public enum ErrorTypes {
        /// <summary>
        /// 未知
        /// </summary>
        Unknow = 0,

        /// <summary>
        /// 取消
        /// </summary>
        Canceled,

        /// <summary>
        /// 数据解析错误
        /// </summary>
        ParseError,

        /// <summary>
        /// API自定义的错误
        /// </summary>
        ResponsedWithErrorInfo,


        /// <summary>
        /// 配置错误
        /// </summary>
        SetupError,

        /// <summary>
        /// 参数验证错误
        /// </summary>
        ParameterError,

        /// <summary>
        /// 未授权
        /// </summary>
        [StatusErrorMap(HttpStatusCode.Unauthorized)]
        UnAuth,



        /// <summary>
        /// 远程服务错误
        /// </summary>
        [StatusErrorMap(
            HttpStatusCode.InternalServerError,
            HttpStatusCode.ServiceUnavailable)]
        ServiceException,

        /// <summary>
        /// 错误的请求
        /// </summary>
        [StatusErrorMap(
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.Forbidden,
            HttpStatusCode.MethodNotAllowed)]
        RequestError,
        Network
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class StatusErrorMapAttribute : Attribute {

        public HttpStatusCode[] Codes {
            get;
            private set;
        }

        public StatusErrorMapAttribute(params HttpStatusCode[] codes) {
            this.Codes = codes;
        }
    }

    public static class HttpStatusToErrorTypes {

        private static Dictionary<HttpStatusCode, ErrorTypes> Map = null;

        static HttpStatusToErrorTypes() {
            Map = new Dictionary<HttpStatusCode, ErrorTypes>();

            var values = Enum.GetValues(typeof(ErrorTypes))
                .Cast<int>();
            foreach (var v in values) {
                var errorType = (ErrorTypes)v;
                var attr = EnumHelper.GetAttribute<ErrorTypes, StatusErrorMapAttribute>(errorType);
                if (attr != null && attr.Codes != null) {
                    foreach (var c in attr.Codes) {
                        if (!Map.ContainsKey(c))
                            Map.Add(c, errorType);
                    }
                }
            }
        }

        public static ErrorTypes? Convert(this HttpStatusCode code) {
            if (Map.ContainsKey(code))
                return Map[code];
            else
                return null;
        }
    }
}
