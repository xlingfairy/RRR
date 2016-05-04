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

    /// <summary>
    /// 帮我送,第二步
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class SendStep2ViewModel : BaseVM {
        public override string Title {
            get {
                return "帮我送";
            }
        }

        public ICommand ShowContacterCmd {
            get;
        }

        public SendStep2ViewModel(INavigationService ns) {

            this.ShowContacterCmd = new Command(() => {
                ns.NavigateToViewModelAsync<ContacterViewModel>();
            });
        }
    }
}
