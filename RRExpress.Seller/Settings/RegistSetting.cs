using RRExpress.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Seller.ViewModels;
using RRExpress.AppCommon.Attributes;

namespace RRExpress.Seller.Settings {

    [Regist(InstanceMode.Singleton, ForType = typeof(ISettingItem))]
    public class RegistSetting : ISettingItem {
        public bool CanUse {
            get {
                //这里应该判断用户是否已经注册过了
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
                return 0;
            }
        }

        public string Title {
            get {
                return "注册成为商家";
            }
        }

        public async Task Execute(SimpleContainer container, INavigationService ns) {
            await ns.NavigateToViewModelAsync<RegistViewModel>();
        }
    }
}
