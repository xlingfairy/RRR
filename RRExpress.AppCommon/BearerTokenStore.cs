using RRExpress.AppCommon;
using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System.Threading.Tasks;

namespace RRExpress {

    public class BearerTokenStore : IWebApiBearerTokenStore {

        private static readonly string KEY = "UserToken";

        private Token Token = null;

        public bool IsValid {
            get {
                return this.Token?.IsValid ?? false;
            }
        }

        public BearerTokenStore() {
            this.GetToken();
        }

        public string GetToken() {
            if (this.Token == null) {
                var token = PropertiesHelper.GetObject<Token>(KEY);
                this.Token = token;
            }
            return this.Token?.AccessToken;
        }

        public async Task UpdateToken(Token token) {
            if (token != null && token.IsValid) {
                PropertiesHelper.SetObject(KEY, token);
                this.Token = token;
                await PropertiesHelper.Save();
            }
        }
    }
}
