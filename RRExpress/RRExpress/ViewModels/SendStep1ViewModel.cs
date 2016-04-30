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

    /// <summary>
    /// 帮我送,第一步
    /// </summary>
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

        private MapViewModel MapVM { get; }

        public ICommand ShowTransportCmd { get; }

        public ICommand NextStepCmd { get; }

        public ICommand ShowMapCmd { get; }


        public DateTime MinDate { get; }
        public DateTime MaxDate { get; }

        public SendStep1ViewModel(SimpleContainer container, INavigationService ns) {
            this.DeliveryTypeVM = container.GetInstance<DeliveryTypeViewModel>();
            this.MapVM = container.GetInstance<MapViewModel>();

            this.ShowTransportCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.DeliveryTypeVM);
            });

            this.NextStepCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<SendStep2ViewModel>();
            });

            this.ShowMapCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<MapViewModel>();
            });

            this.MinDate = DateTime.Now.Hour <= 21 ? DateTime.Now : DateTime.Now.AddDays(1);
            this.MaxDate = this.MinDate.AddDays(3);
        }
    }
}
