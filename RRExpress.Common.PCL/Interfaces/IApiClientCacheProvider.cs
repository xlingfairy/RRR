using System.Threading.Tasks;

namespace RRExpress.Common.Interfaces {
    public interface IApiClientCacheProvider {

        Task Store<T>(BaseMethod<T> mth, T result);
        Task<T> Restore<T>(BaseMethod<T> mth);

    }
}
