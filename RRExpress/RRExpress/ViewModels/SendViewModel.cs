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
    public class SendViewModel : BaseVM {
        public override string Title {
            get {
                return "帮我送";
            }
        }

        public DeliveryTypeViewModel DeliveryTypeVM {
            get;
        }

        public ICommand ShowTransportCmd { get; }

        public SendViewModel(SimpleContainer container, INavigationService ns) {
            this.DeliveryTypeVM = container.GetInstance<DeliveryTypeViewModel>();

            this.ShowTransportCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.DeliveryTypeVM);
            });
        }
    }
}
