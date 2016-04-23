using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RRExpress.Service.Controllers {
    public class NewOrdersController : ApiController {

        public IEnumerable<Order> Get(int page = 0) {
            if (page < 3) {
                var rnd = new Random(25);
                int pageSize = 10;
                return Enumerable.Range(pageSize * page, pageSize)
                    .Select(i => {
                        var request = new Order() {
                            ID = i,
                            DistanceToMe = rnd.Next(0, 5),
                            DistanceToTarget = rnd.Next(10, 50),
                            FromAddr = $"起始地{i}",
                            TargetAddr = $"目的地{i}",
                            GoodsName = $"物品{i}",
                            Status = OrderStatus.New
                        };
                        var time = Math.Ceiling((request.DistanceToTarget * 5) / 30.0);
                        request.Time = $"约{time / 2}小时";
                        request.Price = (decimal)Math.Round(time * 8 / 2);
                        return request;
                    });
            } else {
                return Enumerable.Empty<Order>();
            }
        }

    }
}
