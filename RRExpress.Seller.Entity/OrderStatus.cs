using System;
using System.ComponentModel;

namespace RRExpress.Seller.Entity {

    [Flags]
    public enum OrderStatus {

        /// <summary>
        /// 未付款
        /// </summary>
        [Description("未付款")]
        NonPayment = 1,

        /// <summary>
        /// 新单
        /// </summary>
        [Description("新单")]
        Paymented = 2,

        /// <summary>
        /// 部份发货
        /// </summary>
        [Description("部份发货")]
        ParticalSend = 4,

        /// <summary>
        /// 全部发货
        /// </summary>
        [Description("全部发货")]
        FullSend = 8,

        /// <summary>
        /// 纠纷
        /// </summary>
        [Description("纠纷")]
        HasDispute = 16,

        /// <summary>
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refunded = 32,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Finished = 64,


        [Description("全部")]
        All = NonPayment | Paymented |
            ParticalSend | FullSend |
            HasDispute | Refunded |
            Finished,

        /// <summary>
        /// 等待收货
        /// </summary>
        [Description("待收货")]
        WaitReceive = Paymented | ParticalSend | FullSend,

        /// <summary>
        /// 等待评价
        /// </summary>
        [Description("待评价")]
        WaitComment = Refunded | Finished,

        /// <summary>
        /// 退换货
        /// </summary>
        [Description("退换货")]
        Tuihuan = Refunded | HasDispute,


        /// <summary>
        /// 可在次购买
        /// </summary>
        CanReOrder = All ^ NonPayment ^ HasDispute
    }
}
