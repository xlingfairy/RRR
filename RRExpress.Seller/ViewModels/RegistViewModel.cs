using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Seller.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class RegistViewModel : BaseVM {
        public override string Title {
            get {
                return "申请开店";
            }
        }

        public ICommand ShowBusinessCmd {
            get;
        }

        public MainBussinessViewModel BusinessVM { get; }

        public RegistViewModel(SimpleContainer container) {
            this.BusinessVM = container.GetInstance<MainBussinessViewModel>(); //new MainBussinessViewModel();

            this.ShowBusinessCmd = new Command(async () => {
                await PopupHelper.PopupAsync<MainBussinessViewModel>(this.BusinessVM);
            });
        }
    }
}
