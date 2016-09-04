using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon.Attributes;
using RRExpress.Common;
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
    public class OrderListViewModel : ListBase {

        private string _title;

        public override string Title {
            get {
                return this._title;
            }
        }


        private OrderStatus _status;
        public OrderStatus Status {
            get {
                return this._status;
            }
            set {
                this._status = value;
                this._title = $"我的订单 -- {EnumHelper.GetDescription(value)}";
                this.NotifyOfPropertyChange(() => this.Title);
            }
        }


        public ICommand GoPaymentCmd { get; }

        public ICommand GoCommentCmd { get; }

        public ICommand ReOrderCmd { get; }

        public List<OrderInfo> _Datas
                                = new List<OrderInfo>() {
                                #region
                                new OrderInfo() {
                                    StoreID = 1111,
                                    StoreName = "王店村王二麻子养殖场",
                                    BuyerAccount = "xling",
                                    CreateOn = DateTime.Now.AddDays(-1),
                                    DeliveryType = "其它",
                                    HasPaied = true,
                                    OrderNO = "1125698654",
                                    PaiedOn = DateTime.Now.AddHours(-10),
                                    Receiver = "张三",
                                    ReceiverAddress = "湖南省长沙市中山路23号7栋309室",
                                    ReceiverPhone = "15866666666",
                                    TotalAmount = 125,
                                    Details = new List<SubOrderInfo>() {
                                        new SubOrderInfo() {
                                                Count = 10,
                                                GoodsName = "放养鸡，纯天然，谷物喂养",
                                                Price = 12.5M,
                                                Unit = "斤",
                                                    Thumbnail = "http://img12.360buyimg.com/n0/jfs/t826/241/782271093/373096/bf6330df/55530672Nc62530b2.jpg"
                                        }
                                    },
                                    Status = OrderStatus.Paymented
                                },
                                #endregion
                                #region
                                new OrderInfo() {
                                    StoreID = 1112,
                                    StoreName = "李店村李四养殖场",
                                    BuyerAccount = "xling",
                                    CreateOn = DateTime.Now.AddDays(-1),
                                    DeliveryType = "其它",
                                    HasPaied = true,
                                        OrderNO = "112569125",
                                        PaiedOn = DateTime.Now.AddHours(-10),
                                        Receiver = "张飞",
                                        ReceiverAddress = "湖南省长沙市中山路23号7栋309室",
                                        ReceiverPhone = "15866666666",
                                        TotalAmount = 180,
                                        Details = new List<SubOrderInfo>() {
                                            new SubOrderInfo() {
                                                    Count =2,
                                                    GoodsName = "张飞牛肉",
                                                    Price = 50M,
                                                    Unit = "斤",
                                                        Thumbnail = "http://img12.360buyimg.com/n0/jfs/t826/241/782271093/373096/bf6330df/55530672Nc62530b2.jpg"
                                            },
                                            new SubOrderInfo() {
                                                    Count =2,
                                                    GoodsName = "张飞牛杂",
                                                    Price = 40M,
                                                    Unit = "斤",
                                                        Thumbnail = "http://img12.360buyimg.com/n0/jfs/t826/241/782271093/373096/bf6330df/55530672Nc62530b2.jpg"
                                            }
                                        },
                                        Status = OrderStatus.NonPayment
                                },
                                #endregion
                                #region
                                new OrderInfo() {
                                    StoreID = 1113,
                                    StoreName = "张店村张三养殖场",
                                    BuyerAccount = "xling",
                                    CreateOn = DateTime.Now.AddDays(-1),
                                    DeliveryType = "其它",
                                    HasPaied = true,
                                    OrderNO = "1125698322",
                                    PaiedOn = DateTime.Now.AddHours(-13),
                                    Receiver = "赵四",
                                    ReceiverAddress = "湖南省长沙市香洲路5号15栋102室",
                                    ReceiverPhone = "15866666666",
                                    TotalAmount = 125,
                                    Details = new List<SubOrderInfo>() {
                                        new SubOrderInfo() {
                                                Count = 10,
                                                GoodsName = "放养鸡，纯天然，谷物喂养",
                                                Price = 12.5M,
                                                Unit = "斤",
                                                Thumbnail = "http://img12.360buyimg.com/n0/jfs/t826/241/782271093/373096/bf6330df/55530672Nc62530b2.jpg"
                                        }
                                    },
                                    Status = OrderStatus.Finished
                                },
                                #endregion
                                #region
                                new OrderInfo() {
                                    StoreID = 1115,
                                    StoreName = "赵家楼赵元外养殖场",
                                    BuyerAccount = "xling",
                                    CreateOn = DateTime.Now.AddDays(-1),
                                    DeliveryType = "拜托送",
                                    HasPaied = true,
                                    OrderNO = "1125698755",
                                    PaiedOn = DateTime.Now.AddHours(-10),
                                    Receiver = "李四",
                                    ReceiverAddress = "湖南省长沙市北山路11号7栋302室",
                                    ReceiverPhone = "15866666666",
                                    TotalAmount = 125,
                                    Details = new List<SubOrderInfo>() {
                                        new SubOrderInfo() {
                                                Count = 10,
                                                GoodsName = "放养鸡，纯天然，谷物喂养",
                                                Price = 12.5M,
                                                Unit = "斤",
                                                Thumbnail = "http://img12.360buyimg.com/n0/jfs/t826/241/782271093/373096/bf6330df/55530672Nc62530b2.jpg"
                                        }
                                    },
                                    Status = OrderStatus.HasDispute
                                },
                                #endregion
                                #region
                                new OrderInfo() {
                                    StoreID = 1115,
                                    StoreName = "赵家楼赵元外养殖场",
                                    BuyerAccount = "xling",
                                    CreateOn = DateTime.Now.AddDays(-1),
                                    DeliveryType = "拜托送",
                                    HasPaied = true,
                                    OrderNO = "1125698654",
                                    PaiedOn = DateTime.Now.AddHours(-10),
                                    Receiver = "王二",
                                    ReceiverAddress = "湖南省长沙市东山路12号9栋309室",
                                    ReceiverPhone = "15866666666",
                                    TotalAmount = 80,
                                    Details = new List<SubOrderInfo>() {
                                        new SubOrderInfo() {
                                                Count = 5,
                                                GoodsName = "放养鸡，纯天然，谷物喂养",
                                                Price = 16,
                                                Unit = "斤",
                                                    Thumbnail = "http://img12.360buyimg.com/n0/jfs/t826/241/782271093/373096/bf6330df/55530672Nc62530b2.jpg"
                                        }
                                    },
                                    Status = OrderStatus.FullSend
                                }
                                #endregion
                                };


        public OrderListViewModel() {
            this.GoPaymentCmd = new Command((o) => {

            });

            this.GoCommentCmd = new Command((o) => {

            });

            this.ReOrderCmd = new Command((o) => {

            });
        }

        protected override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            if (page > 0)
                return Task.FromResult(new Tuple<bool, IEnumerable<object>>(false, null));

            var datas = this._Datas.Where(d => (d.Status & this.Status) == d.Status);
            return Task.FromResult(new Tuple<bool, IEnumerable<object>>(false, datas));
        }

        protected async override void OnActivate() {
            base.OnActivate();

            //if (this.Datas == null || this.Datas.Count == 0) {
            await Task.Delay(500)
                .ContinueWith(async t => {
                    await this.LoadData(true);
                });
            //}
        }

        public void ShowDetail(OrderInfo data) {
            IoC.Get<INavigationService>()
               .For<OrderDetailViewModel>()
               .WithParam(p => p.Data, data)
               .Navigate();
        }
    }
}
