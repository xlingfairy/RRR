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

        public IEnumerable<Catalog> Catalogs {
            get;
        }

        public ProductsListViewModel() {
            var cats = new List<string>() {
                "休闲食品","生鲜果蔬","办公家居","鲜花","蛋糕","其它"
            };
            this.Catalogs = cats.Select(c => new Catalog() {
                Name = c
            });
        }

        public class Catalog : ISelectable {

            public string Name { get; set; }

            public bool IsSelected {
                get;
                set;
            }

            public ICommand SelectCommand {
                get; set;
            }

            public void NotifyOfPropertyChange(string propertyName) {

            }
        }
    }
}
