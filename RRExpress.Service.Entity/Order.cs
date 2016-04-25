using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Service.Entity {
    /// <summary>
    /// 新单
    /// </summary>
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields, EnumPassthru = true)]
    public class Order {

        public int ID { get; set; }

        public string OrderNO { get; set; }

        public string GoodsName { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public long DistanceToMe { get; set; }

        public long DistanceToTarget { get; set; }

        public string FromAddr { get; set; }

        public string TargetAddr { get; set; }

        public string Time { get; set; }

        public OrderStatus Status { get; set; }

        /// <summary>
        /// 发货人
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 物品申报价值
        /// </summary>
        public decimal DeclaredValue { get; set; }

        /// <summary>
        /// 运送方式
        /// </summary>
        public string DeliveryType { get; set; }

        public string Remark { get; set; }
    }
}
