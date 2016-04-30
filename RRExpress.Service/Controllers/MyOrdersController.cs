using RRExpress.Common.Extends;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RRExpress.Service.Controllers {
    public class MyOrdersController : ApiController {

        [HttpGet]
        public IEnumerable<Order> Get(OrderStatus status, int page = 0) {
            var arr = status.ToString().Split(',')
                .Select(s => s.ToEnum<OrderStatus>())
                .ToList();


            if (page < 2) {
                var rnd = new Random(25);
                int pageSize = 10;
                var datas = Enumerable.Range(pageSize * page, pageSize)
                    .Select(i => {
                        var request = new Order() {
                            ID = i,
                            OrderNO = i.ToString("0000000000"),
                            DistanceToMe = rnd.Next(0, 5),
                            DistanceToTarget = rnd.Next(10, 50),
                            FromAddr = $"起始地{i}",
                            TargetAddr = $"目的地{i}",
                            GoodsName = $"物品{i}",
                            Status = arr[rnd.Next(0, arr.Count - 1)],
                            Sender = "张山",
                            Consignee = "李四",
                            Qty = 1,
                            DeclaredValue = 500,
                            DeliveryType = "不限",
                            Remark = "内容不限，题材不限"
                        };
                        var time = Math.Ceiling((request.DistanceToTarget * 5) / 30.0);
                        request.Time = $"约{time / 2}小时";
                        request.Price = (decimal)Math.Round(time * 8 / 2);
                        return request;
                    });
                return datas;
            }
            else {
                return Enumerable.Empty<Order>();
            }
        }


        [HttpGet]
        public IEnumerable<OrderEvent> GetEvents(string orderNo) {
            var arr = Enum.GetValues(typeof(OrderStatus)).Cast<int>();
            var rn = new Random();
            var c = rn.Next(1, arr.Count() - 1);
            var time = DateTime.Now.AddDays(rn.Next(-5, -1));
            return Enumerable.Range(0, c)
                .Select(i => {
                    time = time.AddHours(2);
                    return new OrderEvent() {
                        ID = DateTime.Now.Ticks,
                        OrderNO = orderNo,
                        Status = (OrderStatus)arr.ElementAt(i),
                        EventTime = time
                    };
                });
        }
    }
}
