using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RRExpress.Service.Controllers {
    public class VillageController : ApiController {

        [HttpGet]
        //[Route("{province}/{city}/{county}")]
        public IEnumerable<Region> Get(string province, string city, string county) {
            return new List<Region>() {
                new Region() {
                    AreaName = "大马乡",
                     Children = new List<Region>() {
                         new Region() { AreaName = "大马村" },
                         new Region() { AreaName = "小马村" }
                     }
                },
                new Region() {
                    AreaName = "东头乡",
                     Children = new List<Region>() {
                         new Region() { AreaName = "东头村" },
                         new Region() { AreaName = "西头村" }
                     }
                }
            };
        }

    }
}
