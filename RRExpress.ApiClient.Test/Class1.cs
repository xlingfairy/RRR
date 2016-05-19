using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ApiClient.Test {
    [Export(typeof(IWebApiBearerTokenProvider))]
    public class BearTokenProvider : IWebApiBearerTokenProvider {
        public bool IsValid {
            get {
                return true;
            }
        }

        public string GetToken() {
            return "";
        }

        public Task UpdateToken(Token token) {
            return Task.FromResult<object>(null);
        }
    }
}
