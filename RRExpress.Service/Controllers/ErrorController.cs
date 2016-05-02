using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RRExpress.Service.Controllers {
    public class ErrorController : ApiController {

        public string Handle404() {
            return "憋试了,没用!SB!!!";
        }

    }
}
