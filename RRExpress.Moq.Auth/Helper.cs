using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Moq.Auth {
    public static class Helper {

        public static void UseMoqAuth(this IAppBuilder app) {
            //配置授权认证
            app.CreatePerOwinContext(AppUserManager.Create);
            var oauthOptions = new OAuthAuthorizationServerOptions {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new AppOAuthProvider("_SELF"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                //在生产模式下设 AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // 使应用程序可以使用不记名令牌来验证用户身份
            app.UseOAuthBearerTokens(oauthOptions);
        }

    }
}
