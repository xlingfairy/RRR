using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Express.ViewModels {

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


        #region 模型
        /// <summary>
        /// 配送方式模型
        /// </summary>
        public DeliveryTypeViewModel DeliveryTypeVM { get; }

        ///// <summary>
        ///// 地图模型，已不用
        ///// </summary>
        //private MapViewModel MapVM { get; }

        /// <summary>
        /// 区域选择模型
        /// </summary>
        public ChoiceRegionViewModel RegionVM { get; }

        /// <summary>
        /// 配送时间选择模型
        /// </summary>
        public PickupTimeViewModel PickupTimeVM { get; }
        #endregion


        #region Command
        /// <summary>
        /// 显示运送方式
        /// </summary>
        public ICommand ShowTransportCmd { get; }

        /// <summary>
        /// 显示下一步
        /// </summary>
        public ICommand NextStepCmd { get; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public ICommand ShowMapCmd { get; }

        /// <summary>
        /// 显示区域选择 
        /// </summary>
        public ICommand ShowChoiceRegionCmd { get; }

        /// <summary>
        /// 显示时间选择
        /// </summary>
        public ICommand ShowPickupTimeCmd { get; }


        public ICommand ShowContacterCmd {
            get;
        }
        #endregion



        #region 数据
        public Contacter Sender { get; set; }
        public Contacter Receiver { get; set; }

        private PickupTime PickupTime { get; set; }
        private ChoicedRegion SenderRegion { get; set; }
        private ChoicedRegion ReceiverRegion { get; set; }

        //由于XF 的 Binding 没有 TargetNullValue / FallBack ，只能通过另外写属性进行绑定

        public string PickupTimeDesc {
            get {
                return this.PickupTime?.Label ?? "请选择";
            }
        }
        public string SenderRegionDesc {
            get {
                return this.SenderRegion?.FullName ?? "请选择发货地";
            }
        }
        public string ReceiverRegionDesc {
            get {
                return this.ReceiverRegion?.FullName ?? "请选择收货地";
            }
        }
        #endregion


        private string RegionTag = null;
        private string ContacterTag = null;

        public SendStep1ViewModel(SimpleContainer container, INavigationService ns) {
            this.DeliveryTypeVM = container.GetInstance<DeliveryTypeViewModel>();
            this.RegionVM = container.GetInstance<ChoiceRegionViewModel>();
            this.PickupTimeVM = container.GetInstance<PickupTimeViewModel>();

            //送货方式
            this.ShowTransportCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.DeliveryTypeVM);
            });

            //下一步
            this.NextStepCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<SendStep2ViewModel>();
            });

            this.ShowContacterCmd = new Command((o) => {
                this.ContacterTag = (string)o;
                ns.NavigateToViewModelAsync<ContacterViewModel>();
            });


            //区域选择
            this.ShowChoiceRegionCmd = new Command(async (o) => {
                this.RegionTag = (string)o;
                switch (this.RegionTag) {
                    case "S"://发货人
                        this.RegionVM.Update(this.SenderRegion);
                        break;
                    case "R"://收货人
                        this.RegionVM.Update(this.ReceiverRegion);
                        break;
                }
                await PopupHelper.PopupAsync(this.RegionVM);
            });

            //取货时间
            this.ShowPickupTimeCmd = new Command(async () => {
                this.PickupTimeVM.UpdateChoiced(this.PickupTime);
                await PopupHelper.PopupAsync(this.PickupTimeVM);
            });



            #region 消息订阅

            //取货时间
            MessagingCenter.Subscribe<PickupTimeViewModel, PickupTime>(this, PickupTimeViewModel.MESSAGE_KEY, (s, p) => {
                this.PickupTime = p;
                this.NotifyOfPropertyChange(() => this.PickupTimeDesc);
            });

            //区域选择
            MessagingCenter.Subscribe<ChoiceRegionViewModel, ChoicedRegion>(this, ChoiceRegionViewModel.MESSAGE_KEY, (s, p) => {
                switch (this.RegionTag) {
                    case "S"://发货地址
                        this.SenderRegion = p;
                        this.NotifyOfPropertyChange(() => this.SenderRegionDesc);
                        break;
                    case "R"://收货地址
                        this.ReceiverRegion = p;
                        this.NotifyOfPropertyChange(() => this.ReceiverRegionDesc);
                        break;
                }
            });


            MessagingCenter.Subscribe<ContacterViewModel, Contacter>(this, ContacterViewModel.MESSAGE_KEY, (sender, contacter) => {
                switch (this.ContacterTag) {
                    case "S":
                        this.Sender = contacter;
                        this.NotifyOfPropertyChange(() => this.Sender);
                        break;
                    case "R":
                        this.Receiver = contacter;
                        this.NotifyOfPropertyChange(() => this.Receiver);
                        break;
                }
            });
            #endregion
        }
    }
}
