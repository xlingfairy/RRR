using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using RRExpress.Attributes;
using RRExpress.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class SendStep1ViewModel : BaseVM {
        public override string Title {
            get {
                return "帮我送";
            }
        }

        public DeliveryTypeViewModel DeliveryTypeVM {
            get;
        }

        public ICommand ShowTransportCmd { get; }

        public ICommand NextStepCmd { get; }

        public SendStep1ViewModel(SimpleContainer container, INavigationService ns) {
            this.DeliveryTypeVM = container.GetInstance<DeliveryTypeViewModel>();

            this.ShowTransportCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.DeliveryTypeVM);
            });

            this.NextStepCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<SendStep2ViewModel>();
            });
        }
    }
}
