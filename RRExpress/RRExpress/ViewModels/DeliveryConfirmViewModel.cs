using AsNum.XFControls;
using Caliburn.Micro;
using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 确认送达
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class DeliveryConfirmViewModel : BaseVM {
        public override string Title {
            get {
                return "确认送达";
            }
        }

        private Order _data = null;
        /// <summary>
        /// 当前订单
        /// </summary>
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

        /// <summary>
        /// 验证码
        /// </summary>
        private SignByVerifyCodeViewModel ByCodeVM { get; }
        
        /// <summary>
        /// 微信扫码
        /// </summary>
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
