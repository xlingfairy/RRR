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
        #endregion



        #region 数据
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


        public SendStep1ViewModel(SimpleContainer container, INavigationService ns) {
            this.DeliveryTypeVM = container.GetInstance<DeliveryTypeViewModel>();
            //this.MapVM = container.GetInstance<MapViewModel>();
            this.RegionVM = container.GetInstance<ChoiceRegionViewModel>();

            this.ShowTransportCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.DeliveryTypeVM);
            });

            this.NextStepCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<SendStep2ViewModel>();
            });

            //this.ShowMapCmd = new Command(async () => {
            //    await ns.NavigateToViewModelAsync<MapViewModel>();
            //});

            this.ShowChoiceRegionCmd = new Command(async (o) => {
                this.RegionTag = (string)o;
                await PopupHelper.PopupAsync(this.RegionVM);
            });

            this.ShowPickupTimeCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.PickupTimeVM);
            });



            #region 消息订阅
            MessagingCenter.Subscribe<PickupTimeViewModel, PickupTime>(this, PickupTimeViewModel.MESSAGE_KEY, (s, p) => {
                this.PickupTime = p;
                this.NotifyOfPropertyChange(() => this.PickupTimeDesc);
            });

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
            #endregion
        }
    }
}
