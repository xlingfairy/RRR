using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System.Composition;
using System.Threading.Tasks;

namespace RRExpress.Api.V1 {

    [Export(typeof(IClientSetup))]
    public class Setup : IClientSetup {

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
            return BearTokenProvider.Default.GetToken();
        }

        public async Task UpdateToken(Token token) {
            await BearTokenProvider.Default.UpdateToken(token);
        }
    }
}
