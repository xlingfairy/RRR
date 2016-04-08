using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Moq.Auth {
    class AppUserManager : UserManager<AppUser, int> {

        public AppUserManager(IUserStore<AppUser, int> store)
            : base(store) {
        }

        public override async Task<bool> CheckPasswordAsync(AppUser user, string password) {
            var pwd = ToMD5(ToMD5(password));
            var data = await base.CheckPasswordAsync(user, pwd);
            return data;
        }

        protected async override Task<bool> VerifyPasswordAsync(IUserPasswordStore<AppUser, int> store, AppUser user, string password) {
            //base.VerifyPasswordAsync 使用的是 Rfc2898DeriveBytes 密码，
            //而这里，密码是两次MD5加密
            //传入的 password ， 即上面的 CheckPasswordAsync 中两次MD5的结果
            //return await base.VerifyPasswordAsync(store, user, password);

            var hash = await store.GetPasswordHashAsync(user);
            return string.Equals(hash, password);
        }

        internal static AppUserManager Create() {
            return new AppUserManager(new UserStore());
        }

        private static string ToMD5(string input) {
            using (var md5Hasher = MD5.Create()) {
                byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++) {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
