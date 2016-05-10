using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using RRExpress.Models;
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
        }
    }
}
