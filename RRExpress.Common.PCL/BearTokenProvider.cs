using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common {
    public static class BearTokenProvider {

        private static IWebApiBearerTokenStore _default = null;
        public static IWebApiBearerTokenStore Default {
            get {
                if (_default == null)
                    _default = new DefaultBearTokenProvider();
                return _default;
            }
            set {
                _default = value;
            }
        }


        public class DefaultBearTokenProvider : IWebApiBearerTokenStore {

            private Token Token = null;

            public bool IsValid {
                get {
                    return this.Token?.IsValid ?? false;
                }
            }

            public string GetToken() {
                return this.Token?.AccessToken;
            }

            public Task UpdateToken(Token token) {
                return Task.Run(() => {
                    this.Token = token;
                });
            }
        }
    }
}
