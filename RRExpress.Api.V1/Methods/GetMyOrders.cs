using RRExpress.Common.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {

    /// <summary>
    /// 我的订单
    /// </summary>
    public class GetMyOrders : RRExpressV1BaseMethod<IEnumerable<Order>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "MyOrders";
            }
        }

        [Param]
        public int Page { get; set; }

        [Param]
        public OrderStatus Status { get; set; } = OrderStatus.All ^ OrderStatus.New;

        /// <summary>
        /// 发单创建人
        /// </summary>
        [Param]
        public bool AsCreator { get; set; } = false;

        /// <summary>
        /// 发单发货人
        /// </summary>
        [Param]
        public bool AsSender { get; set; } = false;
    }
}
