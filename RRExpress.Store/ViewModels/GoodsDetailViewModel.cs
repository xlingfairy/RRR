using RRExpress.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {
    public class GoodsDetailViewModel : BaseVM {
        public override string Title {
            get {
                return "商品详情";
            }
        }

        public int ID { get; set; }

        public Dictionary<string, string> Imgs { get; set; }
        = new Dictionary<string, string>() {
            { "http://pic28.nipic.com/20130408/12320368_211019854000_2.jpg",""},
            { "http://www.k618.cn/news/xy/201202/W020120209380574066036.jpg",""},
            { "",""}
        };
    }
}
