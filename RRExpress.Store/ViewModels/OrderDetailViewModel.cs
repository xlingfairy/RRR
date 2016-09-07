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

        private IEnumerable<OrderInfo> _data = null;
        /// <summary>
        /// 从API中来的数据,每张单只有一个OrderInfo
        /// </summary>
        public IEnumerable<OrderInfo> Datas {
            get {
                return this._data;
            }
            set {
                this._data = value;
                this.NotifyOfPropertyChange(() => this.Datas);
            }
        }

        private IEnumerable<ShoppingCartItem> _commitDatas = null;
        /// <summary>
        /// 从购物车中提交的订单数据，按店铺拆分成多个 OrderInfo
        /// </summary>
        public IEnumerable<ShoppingCartItem> CommitDatas {
            get {
                return this._commitDatas;
            }
            set {
                this._commitDatas = value;
                this.Datas = value.GroupBy(i => i.Data.StoreID)
                                    .Select(g => {
                                        var d = g.First().Data;

                                        return new OrderInfo {
                                            StoreID = g.Key,
                                            StoreName = d.Name,
                                            CreateOn = DateTime.Now,
                                            OrderNO = "",
                                            TotalAmount = g.Sum(gg => gg.Amount),
                                            Details = g.Select(gg => new SubOrderInfo() {
                                                Count = (int)gg.Count,
                                                GoodsID = gg.Data.ID,
                                                GoodsName = gg.Data.Name,
                                                Price = gg.Data.Price,
                                                Thumbnail = gg.Data.Thumbnail,
                                                Unit = gg.Data.Unit,
                                            }),
                                            Status = OrderStatus.NonPayment,
                                            Receiver = "张三"
                                        };
                                    });
            }
        }

        /// <summary>
        /// 收件人信息
        /// </summary>
        public ReceiverInfo Receiver { get; set; }

        public string Remark { get; set; }
    }
}
