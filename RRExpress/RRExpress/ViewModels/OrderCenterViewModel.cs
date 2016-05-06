using AsNum.XFControls;
using Caliburn.Micro;
using RRExpress.Attributes;
using System.Collections.Generic;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 接单框架页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class OrderCenterViewModel : BaseVM {
        public override string Title {
            get {
                return "接单";
            }
        }

        public List<ISelectable> SubVMs {
            get;
        }

        public OrderCenterViewModel(SimpleContainer container) {
            this.SubVMs = new List<ISelectable>() {
                container.GetInstance<NewOrdersViewModel>(),
                container.GetInstance<PickupViewModel>(),
                container.GetInstance<DeliveryViewModel>()
            };
        }
    }
}
