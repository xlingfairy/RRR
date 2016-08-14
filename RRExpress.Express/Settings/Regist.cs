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
    public class Regist : ISettingItem {
        public bool CanUse {
            get {
                //TODO 要判断是否已经注册过
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
                return "申请自由快递人";
            }
        }

        public async Task Execute(SimpleContainer container, INavigationService ns) {
            await ns.NavigateToViewModelAsync<JoinWizardViewModel>();
        }
    }
}
