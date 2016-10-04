using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using System.Threading.Tasks;

namespace RRExpress.AppCommon {
    public interface ISettingItem {

        /// <summary>
        /// 系统分类，如果为空，则取 CustomCatlog
        /// </summary>
        SettingCatlogs? Catlog {
            get;
        }

        /// <summary>
        /// 自定义分类
        /// </summary>
        string CustomCatlog { get; }

        string Icon { get; }

        string Title { get; }

        int Order { get; }

        bool CanUse { get; }

        Task Execute(SimpleContainer container, INavigationService ns);
    }
}
