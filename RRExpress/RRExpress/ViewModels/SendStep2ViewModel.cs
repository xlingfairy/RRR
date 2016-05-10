using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using RRExpress.Models;
using RRExpress.Services;
using System;
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


        public ICommand ShowAddPriceCmd { get; }

        public ICommand ShowGoodsInfoCmd { get; }

        public ICommand PayCmd { get; }

        public AddPriceViewModel AddPriceVM { get; }

        public GoodsInfoViewModel GoodsInfoVM { get; }

        public SendStep2ViewModel(SimpleContainer container, INavigationService ns) {

            this.AddPriceVM = container.GetInstance<AddPriceViewModel>();
            this.GoodsInfoVM = container.GetInstance<GoodsInfoViewModel>();

            this.ShowAddPriceCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.AddPriceVM);
            });

            this.ShowGoodsInfoCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.GoodsInfoVM);
            });

            this.PayCmd = new Command(async () => {
                var wp = DependencyService.Get<IWXPay>();
                await wp.Pay($"test-{DateTime.Now.ToString("yyyyMMdd_HH:mm:ss")}", 1M);
            });
        }
    }
}
