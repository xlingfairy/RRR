using RRExpress.Common.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {

    /// <summary>
    /// 获取广告
    /// </summary>
    public class GetADs : RRExpressV1BaseMethod<IEnumerable<Ad>> {

        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "Ad";
            }
        }

        [Param]
        public AdTypes Type {
            get; set;
        }
    }
}
