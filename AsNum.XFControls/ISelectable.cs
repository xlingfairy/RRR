using System.ComponentModel;
using System.Windows.Input;

namespace AsNum.XFControls {
    public interface ISelectable {
        bool IsSelected { get; set; }

        /// <summary>
        /// 选中时，触发该命令
        /// </summary>
        ICommand SelectedCommand { get; set; }

        /// <summary>
        /// 取消选中时，触发该命令
        /// </summary>
        ICommand UnSelectedCommand { get; set; }

        void NotifyOfPropertyChange(string propertyName);
    }
}
