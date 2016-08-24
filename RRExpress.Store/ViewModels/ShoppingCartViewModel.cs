using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
    }
}
