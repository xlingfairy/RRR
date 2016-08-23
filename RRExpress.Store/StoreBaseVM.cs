using AsNum.XFControls;
using RRExpress.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRExpress.Store {
    public abstract class StoreBaseVM : BaseVM, ISelectable {
        public bool IsSelected {
            get; set;
        }

        public abstract ICommand SelectedCommand { get; set; }

        public virtual ICommand UnSelectedCommand {
            get; set;
        }

        public abstract char Icon { get; }
    }
}
