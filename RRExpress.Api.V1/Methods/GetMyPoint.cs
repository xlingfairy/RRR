using RRExpress.Common.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {

    /// <summary>
    /// 我的积分
    /// </summary>
    public class GetMyPoint : RRExpressV1BaseMethod<IEnumerable<Point>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "Point";
            }
        }

        [Param]
        public int Page { get; set; }
    }
}
