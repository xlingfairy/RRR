using AsNum.XFControls;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using System.Windows.Input;

namespace RRExpress.Store.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class ProductsListViewModel : BaseVM {
        public override string Title {
            get {
                return "产品列表";
            }
        }

        public IEnumerable<string> Catalogs {
            get;
        }

        public ProductsListViewModel() {
            this.Catalogs = new List<string>() {
                "休闲食品","生鲜果蔬","办公家居","鲜花","蛋糕","其它"
            };
        }
    }
}
