using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common.Exceptions {
    public class MethodExecuteException : Exception {

        public HttpStatusCode StateCode {
            get; set;
        }

        public ErrorTypes ErrorType { get; set; }

        public Uri Uri { get; set; }
    }
}
