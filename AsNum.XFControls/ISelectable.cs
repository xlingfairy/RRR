using System.Windows.Input;

namespace AsNum.XFControls {
    public interface ISelectable {
        bool IsSelected { get; set; }

        ICommand SelectCommand { get; set; }

        ICommand UnSelectedCommand { get; set; }

        void NotifyOfPropertyChange(string propertyName);
    }
}
