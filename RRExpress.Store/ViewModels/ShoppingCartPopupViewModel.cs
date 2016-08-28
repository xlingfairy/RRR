using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ShoppingCartPopupViewModel : BaseVM {
        public override string Title {
            get {
                return "购物车";
            }
        }

        public ShoppingCart Cart {
            get {
                return ShoppingCart.Instance.Value;
            }
        }
    }
}
