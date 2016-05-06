using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace RRExpress.Common {
    public class BearerMessageHandler : MessageProcessingHandler {

        private string Token;

        public BearerMessageHandler(string token)
            : base(new HttpClientHandler()) {
            this.Token = token;
        }

        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken) {
            //添加 Bearer 认证请求头
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken) {
            return response;
        }
    }
}
