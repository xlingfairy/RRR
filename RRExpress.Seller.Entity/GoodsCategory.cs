namespace RRExpress.Seller.Entity {
    public class GoodsCategory {

        public int ID { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }

        public string Banner { get; set; }

        /// <summary>
        /// 0代表根
        /// </summary>
        public int PID { get; set; }
    }
}
