using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyViewModel : BaseVM {
        public override string Title {
            get {
                return "我的信息";
            }
        }

        public ICommand ShowEditCmd { get; }

        public ICommand ShowJoinCmd { get; }

        public ICommand ShowMyOrdersCmd { get; }

        public ICommand ShowMyPointCmd { get; }

        public MyViewModel(SimpleContainer container, INavigationService ns) {
            this.ShowEditCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<EditMyInfoViewModel>();
            });

            this.ShowJoinCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<JoinWizardViewModel>();
            });

            this.ShowMyOrdersCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<MyOrdersViewModel>();
            });

            this.ShowMyPointCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<MyPointsViewModel>();
            });
        }
    }
}
