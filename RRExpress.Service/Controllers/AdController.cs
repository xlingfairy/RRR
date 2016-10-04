using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RRExpress.Service.Controllers {

    public class AdController : ApiController {

        private List<Ad> Ads = new List<Ad>() {
            new Ad() { AdType = AdTypes.MobileAdMiddle, Img = "http://www.jiaojianli.com/wp-content/uploads/2013/12/banner_send.jpg", Title = "AD 1", Href = "http://www.baidu.com", Tag="ABC",ID=0 },
            new Ad() { AdType = AdTypes.MobileAdMiddle, Img = "http://img1.100ye.com/img2/4/1230/627/10845627/msgpic/62872955.jpg", Title = "AD 2" },
            new Ad() { AdType = AdTypes.MobileAdMiddle, Img = "http://static.3158.com/im/image/20140820/20140820022157_32140.jpg", Title = "AD 3" }
        };

        public IEnumerable<Ad> Get(AdTypes type, int count = 5) {
            return this.Ads.Where(a => a.AdType == type);
        }

    }

}
