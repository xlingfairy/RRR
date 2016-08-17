using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.Entity {
    public class SubOrderInfo {

        public long ID { get; set; }

        public string OrderNO { get; set; }

        public long GoodsID { get; set; }

        public string GoodsName { get; set; }

        public decimal Price { get; set; }

        public string Unit { get; set; }

        public int Count { get; set; }

        public string Thumbnail { get; set; }
    }
}
