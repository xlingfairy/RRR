using RRExpress.Common.Attributes;
using RRExpress.Service.Entity;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {
    public class GetMessage : RRExpressV1BaseMethod<Message> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "Message/Get";
            }
        }


        [Param]
        public int ID { get; set; }
    }
}
