using RRExpress.AppCommon.Attributes;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ShoppingCartViewModel : StoreBaseVM {

        public override string Title {
            get {
                return "购物车";
            }
        }

        public override char Icon {
            get {
                return (char)0xf07a;
            }
        }

        public ShoppingCart Cart {
            get {
                return ShoppingCart.Instance.Value;
            }
        }

    }
}
