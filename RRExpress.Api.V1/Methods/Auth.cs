using RRExpress.Common;
using RRExpress.Common.Attributes;
using RRExpress.Common.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace RRExpress.Api.V1.Methods {

    /// <summary>
    /// 发送认证请求
    /// </summary>
    public class Auth : RRExpressV1BaseMethod<Token> {

        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Post;
            }
        }

        public override string Module {
            get {
                return "Token";
            }
        }

        public override ContentTypes ContentType {
            get {
                return ContentTypes.Json;
            }
        }


        [Param]
        public string UserName {
            get; set;
        }

        [Param]
        public string Password {
            get; set;
        }


        [Param("grant_type")]
        public string GrantType {
            get {
                return "password";
            }
        }

        protected override HttpContent GetContent() {
            return this.GetStringContent();
        }

        protected override async Task<Token> Parse(IClientSetup setup, byte[] result) {
            var token = await base.Parse(setup, result);
            await ((Setup)setup).UpdateToken(token);
            return token;
        }
    }
}
