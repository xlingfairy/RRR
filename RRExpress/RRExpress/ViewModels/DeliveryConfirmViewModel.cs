using AsNum.XFControls;
using Caliburn.Micro;
using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class DeliveryConfirmViewModel : BaseVM {
        public override string Title {
            get {
                return "确认送达";
            }
        }

        private Order _data = null;

        public Order Data {
            get {
                return this._data;
            }
            set {
                this._data = value;
                this.NotifyOfPropertyChange(() => this.Data);
            }
        }

        public List<ISelectable> SubVMs {
            get;
        }

        private SignByVerifyCodeViewModel ByCodeVM { get; }
        private SignByWechatViewModel ByWechatVM { get; }

        public DeliveryConfirmViewModel(SimpleContainer container) {
            this.ByCodeVM = container.GetInstance<SignByVerifyCodeViewModel>();
            this.ByWechatVM = container.GetInstance<SignByWechatViewModel>();

            this.SubVMs = new List<ISelectable>() {
                this.ByCodeVM,
                this.ByWechatVM
            };
        }
    }
}
