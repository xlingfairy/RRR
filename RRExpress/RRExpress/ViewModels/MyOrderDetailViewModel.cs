using AsNum.XFControls;
using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System.Windows.Input;

namespace RRExpress.ViewModels {
    /// <summary>
    /// 我的发单,详情子页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class MyOrderDetailViewModel : BaseVM, ISelectable {
        public bool IsSelected {
            get; set;
        }

        public ICommand SelectCommand {
            get; set;
        }

        public override string Title {
            get {
                return "订单详情";
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
    }
}
