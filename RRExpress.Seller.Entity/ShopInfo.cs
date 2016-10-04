namespace RRExpress.Seller.Entity {
    public class ShopInfo {

        public long ID {
            get; set;
        }

        public string ShopName { get; set; }

        public long OwnerID { get; set; }

        public string OwnerName { get; set; }

        public string OwnerAvatar { get; set; }
    }
}
