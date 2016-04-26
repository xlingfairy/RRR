using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

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
