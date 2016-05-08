using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RRExpress.Service.Controllers {
    public class AccountController : ApiController {

        [HttpPost]
        public CommonResult Reg(RegistInfo info) {

            return new CommonResult() {
                IsSuccess = true,
                Msg = "注册成功"
            };
        }


    }
}
