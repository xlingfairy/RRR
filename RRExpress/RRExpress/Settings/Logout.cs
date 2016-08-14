using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.ViewModels;
using RRExpress.Views;

namespace RRExpress.Settings {

    [Regist(InstanceMode.Singleton, ForType = typeof(ISettingItem))]
    public class Logout : ISettingItem {
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
                return "注销";
            }
        }

        public async Task Execute(SimpleContainer container, INavigationService ns) {
            PropertiesHelper.Remove("UserToken");
            await PropertiesHelper.Save();
            await ns.NavigateToViewModelAsync<LoginViewModel>();
            var nav = App.Current.MainPage.Navigation;
            var fp = nav.NavigationStack.First();
            if (!(fp is LoginView)) {
                nav.RemovePage(fp);
            }
        }
    }
}
