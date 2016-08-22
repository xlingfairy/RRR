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
    public class CatalogViewModel : StoreBaseVM {
        public override string Title {
            get {
                return "分类";
            }
        }

        public override ICommand SelectCommand {
            get; set;
        }

        public IEnumerable<string> Catalogs {
            get;
        }

        public override char Icon {
            get {
                return (char)0xf0e8;
            }
        }

        public CatalogViewModel() {
            this.Catalogs = new List<string>() {
                "休闲食品","生鲜果蔬","办公家居","鲜花","蛋糕","其它"
            };
        }
    }
}
