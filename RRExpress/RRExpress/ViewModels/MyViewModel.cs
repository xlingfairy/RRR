using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using RRExpress.Views;
using System.Linq;
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

        public ICommand LogoutCmd { get; }

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

            this.LogoutCmd = new Command(async () => {
                PropertiesHelper.Remove("UserToken");
                await ns.NavigateToViewModelAsync<LoginViewModel>();
                var nav = App.Current.MainPage.Navigation;
                var fp = nav.NavigationStack.First();
                if (!(fp is LoginView)) {
                    nav.RemovePage(fp);
                }
            });
        }
    }
}
