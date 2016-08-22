using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class RootViewModel : BaseVM {

        public override string Title {
            get {
                return "Root";
            }
        }

        public List<StoreBaseVM> Datas {
            get;
        }

        public RootViewModel() {
            this.Datas = new List<StoreBaseVM>() {
                IoC.Get<HomeViewModel>(),
                IoC.Get<CatalogViewModel>(),
                IoC.Get<ShoppingCartViewModel>(),
                IoC.Get<MyViewModel>()
            };
        }
    }
}
