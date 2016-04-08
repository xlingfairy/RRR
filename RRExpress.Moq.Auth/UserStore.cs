using Microsoft.AspNet.Identity;
using RRExpress.Common.Extends;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace RRExpress.Moq.Auth {

    [Obsolete("只用于模拟, 请不要使用")]
    class UserStore : IUserStore<AppUser, int>, IUserPasswordStore<AppUser, int> {

        #region IUserStore
        public Task CreateAsync(AppUser user) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AppUser user) {
            throw new NotImplementedException();
        }

        public void Dispose() {

        }

        public Task<AppUser> FindByIdAsync(int userId) {
            var section = ConfigurationHelper.GetSection<UsersConfig>("Users");

            var u = section.Users.Get(userId);
            if (u != null) {
                var user = new AppUser(u.ID, u.UserName);
                return Task.FromResult(user);
            } else
                return Task.FromResult<AppUser>(null);
        }


        public Task<AppUser> FindByNameAsync(string userName) {
            var section = ConfigurationHelper.GetSection<UsersConfig>("Users");

            var u = section.Users.Get(userName);
            if (u != null) {
                var user = new AppUser(u.ID, u.UserName);
                return Task.FromResult(user);
            } else
                return Task.FromResult<AppUser>(null);
        }

        public Task<string> GetPasswordHashAsync(AppUser user) {
            var section = ConfigurationHelper.GetSection<UsersConfig>("Users");
            var u = section.Users.Get(user.UserName);
            if (u != null) {
                return Task.FromResult(u.Password);
            } else
                return Task.FromResult<string>(null);
        }

        public Task<bool> HasPasswordAsync(AppUser user) {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser user) {
            throw new NotImplementedException();
        }


        #endregion

    }
}
