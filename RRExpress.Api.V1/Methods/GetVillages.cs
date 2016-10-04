using RRExpress.Common.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {
    public class GetVillages : RRExpressV1BaseMethod<IEnumerable<Region>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "Village";
            }
        }

        [Param]
        public string Province { get; set; }

        [Param]
        public string City { get; set; }

        [Param]
        public string County { get; set; }
    }
}
