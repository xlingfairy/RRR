using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Models {
    public class PickupTime {
        public string Label { get; set; }
        public DateTime? Time { get; set; }
        public IEnumerable<PickupTime> Times { get; set; }
    }
}
