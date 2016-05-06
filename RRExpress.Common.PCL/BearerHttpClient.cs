using System.Net.Http;

namespace RRExpress.Common {

    /// <summary>
    /// 使用 Bearer Token 认证的 HttpClient
    /// </summary>
    public class BearerHttpClient : HttpClient {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">Bearer Token</param>
        public BearerHttpClient(string token)
            : base(new BearerMessageHandler(token)) {
        }
    }
}
