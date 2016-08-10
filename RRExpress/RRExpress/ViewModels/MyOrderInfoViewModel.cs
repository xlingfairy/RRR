using RRExpress.AppCommon;
using AsNum.XFControls;
using Caliburn.Micro;
using RRExpress.AppCommon.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 我的发单,详情框架页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class MyOrderInfoViewModel : BaseVM {
        public override string Title {
            get {
                return "帮送详情";
            }
        }

        private Order _data = null;
        public Order Data {
            get {
                return this._data;
            }
            set {
                this._data = value;
                this.StatusVM.Data = value;
                this.DetailVM.Data = value;
                this.NotifyOfPropertyChange(() => this.Data);
            }
        }

        public List<ISelectable> SubVMs { get; }

        private MyOrderStatusViewModel StatusVM = null;
        private MyOrderDetailViewModel DetailVM = null;

        public MyOrderInfoViewModel(SimpleContainer container) {
            this.StatusVM = container.GetInstance<MyOrderStatusViewModel>();
            this.DetailVM = container.GetInstance<MyOrderDetailViewModel>();

            this.SubVMs = new List<ISelectable>() {
                this.StatusVM,
                this.DetailVM
            };
        }
    }
}
