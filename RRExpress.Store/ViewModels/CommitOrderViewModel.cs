using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
using RRExpress.Seller.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

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
                this.NotifyOfPropertyChange(() => this.GroupDatas);
            }
        }

        /// <summary>
        /// 分组数据
        /// </summary>
        public IEnumerable<Grouped<ShoppingCartItem>> GroupDatas {
            get {
                return this.Datas?.ToGroup(g => g.Data.StoreName);
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


        public ReceiverInfo Receiver { get; set; }

        public string Remark { get; set; }

        public ICommand ConfirmOrderCmd { get; }

        public Dictionary<int, string> StockOutOptions {
            get;
        } = new Dictionary<int, string>() {
            {0,"其它商品继续配送，缺货商品退款" },
            {1,"直接取消订单" },
            {2,"与我沟通" }
        };


        public CommitOrderViewModel() {
            this.ConfirmOrderCmd = new Command(() => {
                
            });
        }
    }
}
