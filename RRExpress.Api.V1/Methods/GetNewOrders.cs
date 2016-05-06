using RRExpress.Common.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {

    /// <summary>
    /// 待接单的订单
    /// </summary>
    public class GetNewOrders : RRExpressV1BaseMethod<IEnumerable<Order>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "NewOrders";
            }
        }

        [Param]
        public int Page { get; set; } = 0;
    }
}
