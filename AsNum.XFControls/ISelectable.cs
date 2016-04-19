using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsNum.XFControls {
    public interface ISelectable {
        bool IsSelected { get; set; }

        ICommand SelectCommand { get; set; }

        void NotifyOfPropertyChange(string propertyName);
    }
}
