using AsNum.XFControls;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class HomeViewModel : StoreBaseVM {
        public override char Icon {
            get {
                return (char)0xf015;
            }
        }

        public override ICommand SelectCommand {
            get; set;
        }

        public override string Title {
            get {
                return "首页";
            }
        }

    }
}
