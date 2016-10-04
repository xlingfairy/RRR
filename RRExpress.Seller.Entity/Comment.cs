using System;

namespace RRExpress.Seller.Entity {
    public class Comment {

        public long ID { get; set; }

        public long GID { get; set; }

        public long UID { get; set; }

        public string UserName { get; set; }

        public int Star { get; set; }

        public string Content { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
