using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private MainBussinessViewModel BusinessVM { get; }

        public RegistViewModel() {
            this.BusinessVM = new MainBussinessViewModel();

            this.ShowBusinessCmd = new Command(async () => {
                await PopupHelper.PopupAsync<MainBussinessViewModel>(this.BusinessVM);
            });
        }
    }
}
