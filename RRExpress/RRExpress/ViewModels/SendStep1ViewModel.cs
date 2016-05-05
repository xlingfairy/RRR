using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using RRExpress.Attributes;
using RRExpress.Models;
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




        public DeliveryTypeViewModel DeliveryTypeVM { get; }

        private MapViewModel MapVM { get; }

        public ChoiceRegionViewModel RegionVM { get; }

        public PickupTimeViewModel PickupTimeVM { get; }





        public ICommand ShowTransportCmd { get; }

        public ICommand NextStepCmd { get; }

        public ICommand ShowMapCmd { get; }

        public ICommand ShowChoiceRegionCmd { get; }

        public ICommand ShowPickupTimeCmd { get; }





        public PickupTime PickupTime { get; set; }
        public ChoicedRegion SenderRegion { get; set; }
        public ChoicedRegion ReceiverRegion { get; set; }


        private string RegionTag = null;


        public SendStep1ViewModel(SimpleContainer container, INavigationService ns) {
            this.DeliveryTypeVM = container.GetInstance<DeliveryTypeViewModel>();
            this.MapVM = container.GetInstance<MapViewModel>();
            this.RegionVM = container.GetInstance<ChoiceRegionViewModel>();

            this.ShowTransportCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.DeliveryTypeVM);
            });

            this.NextStepCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<SendStep2ViewModel>();
            });

            this.ShowMapCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<MapViewModel>();
            });

            this.ShowChoiceRegionCmd = new Command(async (o) => {
                this.RegionTag = (string)o;
                await PopupHelper.PopupAsync(this.RegionVM);
            });

            this.ShowPickupTimeCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.PickupTimeVM);
            });




            MessagingCenter.Subscribe<PickupTimeViewModel, PickupTime>(this, PickupTimeViewModel.MESSAGE_KEY, (s, p) => {
                this.PickupTime = p;
                this.NotifyOfPropertyChange(() => this.PickupTime);
            });

            MessagingCenter.Subscribe<ChoiceRegionViewModel, ChoicedRegion>(this, ChoiceRegionViewModel.MESSAGE_KEY, (s, p) => {
                switch (this.RegionTag) {
                    case "S":
                        this.SenderRegion = p;
                        this.NotifyOfPropertyChange(() => this.SenderRegion);
                        break;
                    case "R":
                        this.ReceiverRegion = p;
                        this.NotifyOfPropertyChange(() => this.ReceiverRegion);
                        break;
                }
            });
        }
    }
}
