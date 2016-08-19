using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.ViewModels {

    /// <summary>
    /// 界面控件有BUG，导至多进入几次就会崩溃，
    /// 这里使用 PreRequest 避免崩溃
    /// 原因待查
    /// </summary>
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
