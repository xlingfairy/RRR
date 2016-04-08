using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ApiClient {
    public enum ErrorTypes {
        Unknow = 0,

        Network,

        ParseError,

        [StatusErrorMap(HttpStatusCode.Unauthorized)]
        UnAuth,

        [StatusErrorMap(
            HttpStatusCode.InternalServerError,
            HttpStatusCode.ServiceUnavailable)]
        ServiceException,

        ServiceUnavaliable,

        [StatusErrorMap(
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.Forbidden,
            HttpStatusCode.MethodNotAllowed)]
        RequestError
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class StatusErrorMapAttribute : Attribute {

        public HttpStatusCode[] Codes { get; private set; }

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
