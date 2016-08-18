using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class OrderListViewModel : ListBase {
        public override string Title {
            get {
                return "订单列表";
            }
        }

        public List<OrderInfo> _Datas
                                = new List<OrderInfo>() {
                                #region
                                new OrderInfo() {
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

        protected override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            return Task.FromResult(new Tuple<bool, IEnumerable<object>>(false, this._Datas));
        }

        protected async override void OnActivate() {
            base.OnActivate();

            if (this.Datas == null || this.Datas.Count == 0) {
                await Task.Delay(500)
                    .ContinueWith(async t => {
                        await this.LoadData(true);
                    });
            }
        }
    }
}
