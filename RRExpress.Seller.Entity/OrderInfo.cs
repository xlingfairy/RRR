using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.Entity {
    public class OrderInfo {

        public string OrderNO { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal BaseAmount { get; set; }

        public decimal DeliveryFee { get; set; }

        public long StoreID { get; set; }

        public string StoreName { get; set; }

        public long BuyerID { get; set; }

        public string BuyerAccount { get; set; }

        public string Receiver { get; set; }

        public string ReceiverAddress { get; set; }

        public string ReceiverPhone { get; set; }

        public string DeliveryType { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime? PaiedOn { get; set; }

        public IEnumerable<SubOrderInfo> Details { get; set; }

        public int GoodsCount {
            get {
                return this.Details?.Count() ?? 0;
            }
        }

        public OrderStatus Status { get; set; }
    }
}
