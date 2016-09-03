using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class CommitOrderViewModel : BaseVM {
        public override string Title {
            get {
                return "订单结算";
            }
        }

        private IEnumerable<ShoppingCartItem> _datas = null;
        public IEnumerable<ShoppingCartItem> Datas {
            get {
                return this._datas;
            }
            set {
                this._datas = value;
                this.NotifyOfPropertyChange(() => this.BaseAmount);
                this.NotifyOfPropertyChange(() => this.Amount);
            }
        }

        public decimal BaseAmount {
            get {
                return this.Datas?.Sum(d => d.Amount) ?? 0;
            }
        }

        private decimal _deliveryFee = 0;
        public decimal DeliveryFee {
            get {
                return this._deliveryFee;
            }
            set {
                this._deliveryFee = value;
                this.NotifyOfPropertyChange(() => this.Amount);
            }
        }

        public decimal Amount {
            get {
                return this.BaseAmount + this.DeliveryFee;
            }
        }

        public Dictionary<int, string> StockOutOptions {
            get;
        } = new Dictionary<int, string>() {
            {0,"其它商品继续配送，缺货商品退款" },
            {1,"直接取消订单" },
            {2,"与我沟通" },
        };
    }
}
