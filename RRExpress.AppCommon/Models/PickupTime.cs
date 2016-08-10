using System;
using System.Collections.Generic;

namespace RRExpress.AppCommon.Models {

    /// <summary>
    /// 取货时间
    /// </summary>
    public class PickupTime {
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 父标签，仅用于数据还原
        /// </summary>
        public string ParentLabel { get; set; }

        /// <summary>
        /// 标签对应的时间
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// 子时间（仅用第一级）
        /// </summary>
        public IEnumerable<PickupTime> Times { get; set; }
    }
}
