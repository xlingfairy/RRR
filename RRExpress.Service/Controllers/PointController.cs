using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RRExpress.Service.Controllers {
    public class PointController : ApiController {

        public IEnumerable<Point> Get(int page) {
            if (page == 0) {
                return new List<Point>() {
                    new Point () { ID=0, PointType = PointTypes.Regist, Value=50, Time = DateTime.Now.AddDays(-10), Desc="注册积分" },
                    new Point () { ID=1, PointType = PointTypes.Order, Value=20, Time = DateTime.Now.AddDays(-10), Desc="订单001452360积分" },
                    new Point () { ID=2, PointType = PointTypes.Evaluate, Value=10, Time = DateTime.Now.AddDays(-10), Desc="订单001452360评论积分" },
                    new Point () { ID=3, PointType = PointTypes.Order, Value=20, Time = DateTime.Now.AddDays(-10), Desc="订单001452458积分" },
                };
            }
            else
                return null;
        }

    }
}
