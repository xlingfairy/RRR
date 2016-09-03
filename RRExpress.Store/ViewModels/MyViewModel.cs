using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
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

    [Regist(InstanceMode.Singleton)]
    public class MyViewModel : StoreBaseVM {
        public override char Icon {
            get {
                return (char)0xf007;
            }
        }

        public override string Title {
            get {
                return "我的";
            }
        }

        public ICommand ShowOrdersCmd { get; }

        //public Dictionary<OrderStatus, int?> OrderSummary {
        //    get;
        //} = new Dictionary<OrderStatus, int?>() {
        //    { OrderStatus.NonPayment, 1 },
        //    { OrderStatus.WaitReceive, 2 },
        //    { OrderStatus.WaitComment, 10 },
        //    { OrderStatus.Tuihuan, null }
        //};

        public IEnumerable<Tmp> OrderSummary {
            get;
        }

        public MyViewModel() {

            this.OrderSummary = new List<Tmp>() {
                new Tmp() {
                    Status = OrderStatus.All,
                    Count = null,
                    Icon = (char)0xf03a
                },
                new Tmp() {
                    Status = OrderStatus.NonPayment,
                    Count = null,
                    Icon = (char)0xf09d
                },
                new Tmp() {
                    Status = OrderStatus.WaitReceive,
                    Count = null,
                    Icon = (char)0xf0d1
                },
                new Tmp() {
                    Status = OrderStatus.WaitComment,
                    Count = null,
                    Icon = (char)0xf27b
                },
                new Tmp() {
                    Status = OrderStatus.Tuihuan,
                    Count = null,
                    Icon = (char)0xf119
                },
            };

            this.ShowOrdersCmd = new Command((o) => {
                var tmp = (Tmp)o;

                IoC.Get<INavigationService>()
                            .For<OrderListViewModel>()
                            .WithParam(vm => vm.Status, tmp.Status)
                            .Navigate();
            });
        }



        public class Tmp {
            public OrderStatus Status { get; set; }
            public char Icon { get; set; }
            public int? Count { get; set; }
        }
    }
}
