using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class OrderDetailViewModel : BaseVM {

        public override string Title {
            get {
                return "订单详情";
            }
        }

        private OrderInfo _data = null;
        public OrderInfo Data {
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
