using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;

namespace RRExpress.Express.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class MapViewModel : BaseVM {
        public override string Title {
            get {
                return "地图";
            }
        }
    }
}
