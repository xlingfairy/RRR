using RRExpress.Service.Entity;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {
    public class Regist : RRExpressV1BaseMethod<CommonResult> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Post;
            }
        }

        public override string Module {
            get {
                return "Account/Reg";
            }
        }

        public RegistInfo Info {
            get; set;
        }

        protected override object GetSendData() {
            return this.Info;
        }
    }
}
