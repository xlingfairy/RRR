using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.Entity {
    public class GoodsInfo {

        public long ID { get; set; }

        public string Name { get; set; }

        public int BigCat { get; set; }

        public int SecondCat { get; set; }

        public int Channel { get; set; }

        public int? Stock { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? PublishOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public long Owner { get; set; }

        public string Thumbnail { get; set; }

        public bool IsPublished { get; set; }

        public string Desc { get; set; }

        public int SaleVolumeByMonth { get; set; }

        public float PraiseRate { get; set; }

        public int CommentCount { get; set; }

        public decimal? OrgPrice { get; set; }
    }
}
