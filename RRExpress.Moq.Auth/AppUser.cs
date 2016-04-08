using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace RRExpress.Moq.Auth {
    class AppUser : IUser<int> {
        public int Id {
            get;
        }

        public string UserName {
            get; set;
        }

        public AppUser(int id, string userName) {
            this.Id = Id;
            this.UserName = userName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager manager, string authenticationType) {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}
