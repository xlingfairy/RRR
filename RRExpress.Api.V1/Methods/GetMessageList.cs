using RRExpress.Common.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {
    public class GetMessageList : RRExpressV1BaseMethod<IEnumerable<Message>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "Message/GetByPage";
            }
        }

        [Param]
        public int Page { get; set; } = 0;

        [Param]
        public int PageSize { get; set; } = 10;
    }
}
