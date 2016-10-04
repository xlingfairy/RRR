using RRExpress.Common.Interfaces;
using System.Threading.Tasks;

namespace RRExpress.Common {
    public static class ApiClientCacheProvider {

        private static IApiClientCacheStore _default = null;
        public static IApiClientCacheStore Default {
            get {
                if (_default == null)
                    _default = new DefaultApiClientCacheProvider();
                return _default;
            }
            set {
                _default = value;
            }
        }

        public class DefaultApiClientCacheProvider : IApiClientCacheStore {
            public Task<T> Restore<T>(BaseMethod<T> mth) {
                return Task.FromResult<T>(default(T));
            }

            public Task Store<T>(BaseMethod<T> mth, T result) {
                return Task.FromResult<object>(null);
            }
        }
    }
}
