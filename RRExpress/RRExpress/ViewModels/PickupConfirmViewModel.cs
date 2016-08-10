using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Service.Entity;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 取货确认页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class PickupConfirmViewModel : BaseVM {
        public override string Title {
            get {
                return "确认取货";
            }
        }

        public Order Data { get; set; }


    }
}
