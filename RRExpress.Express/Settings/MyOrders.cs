using RRExpress.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Express.ViewModels;
using RRExpress.AppCommon.Attributes;

namespace RRExpress.Express.Settings {

    [Regist(InstanceMode.Singleton, ForType = typeof(ISettingItem))]
    public class MyOrders : ISettingItem {
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
                return 1;
            }
        }

        public string Title {
            get {
                return "我的发单";
            }
        }

        public async Task Execute(SimpleContainer container, INavigationService ns) {
            await ns.NavigateToViewModelAsync<MyOrdersViewModel>();
        }
    }
}
