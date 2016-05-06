using RRExpress.Common.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace RRExpress.Api.V1.Methods {

    /// <summary>
    /// 获取订单操作记录
    /// </summary>
    public class GetOrderEvents : RRExpressV1BaseMethod<IEnumerable<OrderEvent>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "MyOrders/GetEvents";
            }
        }

        /// <summary>
        /// 订单号
        /// </summary>
        [Param(Required = true)]
        public string OrderNO { get; set; }
    }
}
