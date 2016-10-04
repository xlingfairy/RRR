using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;

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
