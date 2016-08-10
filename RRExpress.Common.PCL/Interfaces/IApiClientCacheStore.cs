using System.Threading.Tasks;

namespace RRExpress.Common.Interfaces {
    public interface IApiClientCacheStore {

        Task Store<T>(BaseMethod<T> mth, T result);
        Task<T> Restore<T>(BaseMethod<T> mth);

    }
}
