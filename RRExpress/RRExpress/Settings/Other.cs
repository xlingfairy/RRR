using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System.Threading.Tasks;

namespace RRExpress.Settings {

    [Regist(InstanceMode.Singleton, ForType = typeof(ISettingItem))]
    public class Other : ISettingItem {
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
                return 0;
            }
        }

        public string Title {
            get {
                return "设置";
            }
        }

        public Task Execute(SimpleContainer container, INavigationService ns) {
            return Task.FromResult<object>(null);
        }
    }
}
