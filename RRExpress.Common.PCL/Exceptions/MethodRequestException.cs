using System;
using System.Net;

namespace RRExpress.Common.Exceptions {
    /// <summary>
    /// 发送方法请求时发生的异常
    /// </summary>
    public class MethodRequestException : Exception {

        /// <summary>
        /// 返回的状态码
        /// </summary>
        public HttpStatusCode StateCode {
            get; set;
        }

        public ErrorTypes ErrorType { get; set; }

        public Uri Uri { get; set; }
    }
}
