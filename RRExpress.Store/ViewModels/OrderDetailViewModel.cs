using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class OrderDetailViewModel : BaseVM {

        public override string Title {
            get {
                return "订单详情";
            }
        }

        private OrderInfo _data = null;
        /// <summary>
        /// 从API中来的数据,每张单只有一个OrderInfo
        /// </summary>
        public OrderInfo Data {
            get {
                return this._data;
            }
            set {
                this._data = value;
                this.NotifyOfPropertyChange(() => this.Data);
            }
        }


        public ICommand GoPaymentCmd { get; }

        public ICommand GoCommentCmd { get; }

        public ICommand ReOrderCmd { get; }

        public ICommand CancelOrderCmd { get; }


        public OrderDetailViewModel() {

            this.GoPaymentCmd = new Command(() => {

            });

            this.GoCommentCmd = new Command(() => {

            });

            this.ReOrderCmd = new Command(() => {

            });

            this.CancelOrderCmd = new Command(() => {

            });

        }


    }
}
