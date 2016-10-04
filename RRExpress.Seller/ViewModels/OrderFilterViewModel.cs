using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;

namespace RRExpress.Seller.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class OrderFilterViewModel : BaseVM {
        public override string Title {
            get {
                return "订单过滤";
            }
        }
    }
}
