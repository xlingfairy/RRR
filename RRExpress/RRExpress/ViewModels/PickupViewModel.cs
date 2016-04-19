using AsNum.XFControls;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class PickupViewModel : BaseVM, ISelectable {

        public bool IsSelected {
            get; set;
        }

        public ICommand SelectCommand {
            get; set;
        }

        public override string Title {
            get {
                return "收货";
            }
        }
    }
}
