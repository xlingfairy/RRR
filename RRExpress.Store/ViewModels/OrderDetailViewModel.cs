using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
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
                IoC.Get<INavigationService>()
                    .For<PaymentViewModel>()
                    .WithParam(v => v.OrderNO, this.Data.OrderNO)
                    .WithParam(v => v.TotalAmount, this.Data.TotalAmount)
                    .Navigate();

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
