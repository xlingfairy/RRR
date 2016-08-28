using Caliburn.Micro;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using AsNum.XFControls.Services;
using Caliburn.Micro.Xamarin.Forms;

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
