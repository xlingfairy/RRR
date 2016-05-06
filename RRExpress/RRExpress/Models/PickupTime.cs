using System;
using System.Collections.Generic;

namespace RRExpress.Models {

    /// <summary>
    /// 取货时间
    /// </summary>
    public class PickupTime {
        public string Label { get; set; }
        public DateTime? Time { get; set; }
        public IEnumerable<PickupTime> Times { get; set; }
    }
}
