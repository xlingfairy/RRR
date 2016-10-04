using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Seller.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyGoodsViewModel : BaseVM {
        public override string Title {
            get {
                return "我的商品";
            }
        }

        public BaseVM MasterVM { get; }

        public BaseVM DetailVM { get; }

        public bool IsShowFilter { get; set; }

        public ICommand ShowFilterCmd { get; }

        public MyGoodsViewModel() {
            this.DetailVM = IoC.Get<MyGoodsListViewModel>();
            this.MasterVM = IoC.Get<MyGoodsFilterViewModel>();

            this.ShowFilterCmd = new Command(() => {
                this.IsShowFilter = !this.IsShowFilter;
                this.NotifyOfPropertyChange(() => this.IsShowFilter);
            });
        }
    }
}
