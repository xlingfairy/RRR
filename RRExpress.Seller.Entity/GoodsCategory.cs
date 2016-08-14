using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.Entity {
    public class GoodsCategory {
        public int ID { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 0代表根
        /// </summary>
        public int PID { get; set; }
    }
}
