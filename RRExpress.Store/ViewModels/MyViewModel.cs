using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyViewModel : StoreBaseVM {
        public override char Icon {
            get {
                return (char)0xf007;
            }
        }

        public override string Title {
            get {
                return "我的";
            }
        }
    }
}
