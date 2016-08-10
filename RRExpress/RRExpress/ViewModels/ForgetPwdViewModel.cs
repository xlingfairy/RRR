using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ForgetPwdViewModel : BaseVM {
        public override string Title {
            get {
                return "找回密码";
            }
        }
    }
}
