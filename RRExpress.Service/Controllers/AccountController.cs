using RRExpress.Service.Entity;
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
