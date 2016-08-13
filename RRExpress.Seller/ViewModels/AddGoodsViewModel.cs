using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class AddGoodsViewModel : BaseVM {

        public override string Title {
            get {
                return "发布商品";
            }
        }

        public List<string> DeliveryTypes {
            get;
        } = new List<string>() {
            "平台送", "其它"
        };

        public bool ContainsDeliveryFee { get; set; } = true;

        public string DeliveryType { get; set; }

        public AddGoodsViewModel() {
            this.DeliveryType = this.DeliveryTypes.First();
        }
    }
}
