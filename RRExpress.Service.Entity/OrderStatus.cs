using System;

namespace RRExpress.Service.Entity {
    [Flags]
    public enum OrderStatus {

        /// <summary>
        /// 新请求
        /// </summary>
        New = 1,
        /// <summary>
        /// 已接单
        /// </summary>
        Geted = 2,
        /// <summary>
        /// 已收货
        /// </summary>
        Picked = 4,
        /// <summary>
        /// 已送达
        /// </summary>
        Deliveried = 8,
        /// <summary>
        /// 产生纠纷
        /// </summary>
        Dispute = 16,

        /// <summary>
        /// 已支付
        /// </summary>
        Paied = 32,

        /// <summary>
        /// 已结束
        /// </summary>
        Finished = 64,

        All = New | Geted | Picked | Deliveried | Dispute | Paied | Finished
    }
}
