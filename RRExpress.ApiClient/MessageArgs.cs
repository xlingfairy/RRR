using RRExpress.Common;
using System;

namespace RRExpress.ApiClient {
    public sealed class MessageArgs : EventArgs {
        public ErrorTypes? ErrorType {
            get; set;
        }

        public string Message { get; set; }
    }
}
