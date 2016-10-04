using System.Collections.Generic;

namespace RRExpress.Seller.Entity {

    public class RegitsInfo {
        public string Address { get; set; }

        public string RealName { get; set; }

        public string ShopName { get; set; }

        public string MainBussiness { get; set; }

        public string Phone { get; set; }

        public IEnumerable<byte> Photos { get; set; }

        public bool IsAccept { get; set; }
    }
}
