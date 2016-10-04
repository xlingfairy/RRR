using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class RecommendViewModel : BaseVM {
        public override string Title {
            get {
                return "有奖推荐";
            }
        }


        public string OrderNO { get; set; }

    }
}
