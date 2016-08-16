using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.Models {
    public class GoodsFilter {
        public int BigCat { get; set; }
        public int Channel { get; set; }
        public string SortType { get; set; }
        public string Name { get; set; }
    }
}
