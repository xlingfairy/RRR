using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress {

    [Export(typeof(IBearerTokenProvider))]
    public class BearerTokenProvider : IBearerTokenProvider {

        private Dictionary<string, Token> Tokens = new Dictionary<string, Token>();


        public string GetToken(string tag) {
            var key = $"UserToken.{tag}";
            if (!this.Tokens.ContainsKey(key)) {
                var token = PropertiesHelper.Get<Token>(key);
                this.Tokens.Add(key, token);
            }

            return this.Tokens[key]?.AccessToken;
        }

        public async Task UpdateToken(string tag, Token token) {
            if (token != null && token.IsValid && token.IsLoginedSuccess) {
                PropertiesHelper.Set($"UserToken.{tag}", token);
                await PropertiesHelper.Save();
            }
        }
    }
}
