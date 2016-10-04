using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.ViewModels;
using System.Threading.Tasks;

namespace RRExpress.Seller.Settings {

    [Regist(InstanceMode.Singleton, ForType = typeof(ISettingItem))]
    public class MyOrderSetting : ISettingItem {
        public bool CanUse {
            get {
                return true;
            }
        }

        public SettingCatlogs? Catlog {
            get {
                return null;
            }
        }

        public string CustomCatlog {
            get {
                return Const.SettingCatlog;
            }
        }

        public string Icon {
            get {
                return null;
            }
        }

        public int Order {
            get {
                return 3;
            }
        }

        public string Title {
            get {
                return "我的订单";
            }
        }

        public async Task Execute(SimpleContainer container, INavigationService ns) {
            await ns.NavigateToViewModelAsync<OrdersViewModel>();
        }
    }
}
