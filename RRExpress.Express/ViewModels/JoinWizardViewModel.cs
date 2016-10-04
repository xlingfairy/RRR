using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Express.ViewModels {

    /// <summary>
    /// 
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class JoinWizardViewModel : BaseVM {
        public override string Title {
            get {
                return "注册自由快递员";
            }
        }

        public ICommand GotoRegistCmd { get; }

        public JoinWizardViewModel(INavigationService ns) {
            this.GotoRegistCmd = new Command(() => {
                ns.NavigateToViewModelAsync<JoinViewModel>();
            });
        }
    }
}
