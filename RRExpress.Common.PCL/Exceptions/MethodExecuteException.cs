using System;
using System.Net;

namespace RRExpress.Common.Exceptions {

    /// <summary>
    /// API方法执行返回异常（500）
    /// </summary>
    public class MethodExecuteException : Exception {

        public HttpStatusCode StateCode {
            get; set;
        }

        public ErrorTypes ErrorType { get; set; }

        public Uri Uri { get; set; }
    }
}
