using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class PaymentViewModel : BaseVM {
        public override string Title {
            get {
                return "支付";
            }
        }

        public string OrderNO { get; set; }


        private decimal _totalAmount;
        public decimal TotalAmount {
            get {
                return this._totalAmount;
            }
            set {
                this._totalAmount = value;
                this.NotifyOfPropertyChange(() => this.TotalAmount);
            }
        }

        public List<Tmp> Datas {
            get;
        } = new List<Tmp>() {
            new Tmp() {
                Name = "微信支付",
                Desc = "推荐安装微信5.0及以上版本使用"
            },
            new Tmp() {
                Name = "余额支付",
                Desc = "当前余额：￥108.00"
            }
        };

        public class Tmp {
            public string Name { get; set; }

            public string Desc { get; set; }
        }
    }
}
