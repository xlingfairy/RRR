using AsNum.XFControls;
using Caliburn.Micro;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

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
                container.GetInstance<NewRequestViewModel>(),
                container.GetInstance<PickupViewModel>(),
                container.GetInstance<DeliveryViewModel>()
            };
        }
    }
}
