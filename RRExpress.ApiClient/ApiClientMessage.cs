using System;

namespace RRExpress.ApiClient {
    public sealed class ApiClientMessageArgs : EventArgs {
        public ErrorTypes? ErrorType {
            get; set;
        }

        public string Message { get; set; }
    }
}
