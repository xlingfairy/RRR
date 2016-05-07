using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace RRExpress {

    /// <summary>
    /// 因为在 Api 程序集内，不方便实现文件存储（不依赖相关程序集），
    /// 所以把它放到 App 内实现
    /// 并通过 MEF 导入（API Client 是通过 MEF 实现的）
    /// </summary>
    [Export(typeof(IWebApiBearerTokenProvider))]
    [Shared]
    public class BearerTokenProvider : IWebApiBearerTokenProvider {

        private static readonly string KEY = "UserToken";

        private Token Token = null;

        public bool IsValid {
            get {
                return this.Token?.IsValid ?? false;
            }
        }

        public BearerTokenProvider() {
            Debug.WriteLine("---------------------------");
        }

        public string GetToken() {
            if (this.Token == null) {
                var token = PropertiesHelper.Get<Token>(KEY);
                this.Token = token;
            }
            return this.Token?.AccessToken;
        }

        public async Task UpdateToken(Token token) {
            if (token != null && token.IsValid) {
                PropertiesHelper.Set(KEY, token);
                this.Token = token;
                await PropertiesHelper.Save();
            }
        }
    }
}
