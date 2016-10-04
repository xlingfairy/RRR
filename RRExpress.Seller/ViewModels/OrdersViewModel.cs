using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Seller.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class OrdersViewModel : BaseVM {
        public override string Title {
            get {
                return "我的订单";
            }
        }

        public BaseVM MasterVM { get; }

        public BaseVM DetailVM { get; }

        public bool IsShowFilter { get; set; }

        public ICommand ShowFilterCmd { get; }

        public OrdersViewModel() {
            this.DetailVM = IoC.Get<OrderListViewModel>();
            this.MasterVM = IoC.Get<OrderFilterViewModel>();

            this.ShowFilterCmd = new Command(() => {
                this.IsShowFilter = !this.IsShowFilter;
                this.NotifyOfPropertyChange(() => this.IsShowFilter);
            });
        }
    }
}
