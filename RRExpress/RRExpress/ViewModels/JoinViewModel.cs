using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class JoinViewModel : BaseVM {
        public override string Title {
            get {
                return "加入自由快递人";
            }
        }
    }
}
