using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

        //TODO 必须设置,并绑定,否则在进入的话,会有两个选中的
        // TabView Bug 未找到
        public StoreBaseVM SelectedVM { get; set; }


        private bool _ShowNavigationBar = true;
        /// <summary>
        /// 是否显示顶部导航栏
        /// </summary>
        public bool ShowNavigationBar {
            get {
                return this._ShowNavigationBar;
            }
            set {
                this._ShowNavigationBar = value;
                this.NotifyOfPropertyChange(() => this.ShowNavigationBar);
            }
        }

        public int ShoppingCartCount { get; set; }

        public RootViewModel() {
            this.Datas = new List<StoreBaseVM>() {
                IoC.Get<HomeViewModel>(),
                //IoC.Get<CatalogViewModel>(),
                IoC.Get<ShoppingCartViewModel>(),
                IoC.Get<MyViewModel>()
            };
        }
    }
}
