using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System.Composition;
using System.Threading.Tasks;

namespace RRExpress.Api.V1 {

    [Export(typeof(IClientSetup))]
    public class Setup : IClientSetup {


        /// <summary>
        /// MEF 导入 , 因为在这里不方便存储，需要在外部实现 IBearerToken, 并导出
        /// </summary>
        [Import]
        public IWebApiBearerTokenProvider TokenProvider {
            get; set;
        }


        public bool IsValid {
            get {
                return true;
            }
        }


        /// <summary>
        /// 请求超时 秒
        /// </summary>
        public int Timeout {
            get; set;
        } = 30;

        /// <summary>
        /// 测试环境的API基地址
        /// </summary>
        private string SandboxBaseUri = "http://rr.tunnel.qydev.com/api/";


        //TODO 修改正式环境的基地址
        /// <summary>
        /// 正式环境的API基地址
        /// </summary>
        private string BaseUri = "http://www.baidu.com/api/";


        public string GetUrl(BaseMethod mth, bool useSandbox) {
            if (useSandbox)
                return $"{this.SandboxBaseUri}/{mth.Module}";
            else
                return $"{this.BaseUri}/{mth.Module}";
        }

        public string GetToken() {
            return this.TokenProvider.GetToken();
        }

        public async Task UpdateToken(Token token) {
            await this.TokenProvider.UpdateToken(token);
        }
    }
}
