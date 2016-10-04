using Newtonsoft.Json;
using PCLStorage;
using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System.Threading.Tasks;

namespace RRExpress.AppCommon {

    public class ApiClientCacheStore : IApiClientCacheStore {
        public async Task<T> Restore<T>(BaseMethod<T> mth) {
            var folder = await FileSystem.Current.LocalStorage.CreateFolderAsync("ApiCache", CreationCollisionOption.OpenIfExists);
            var key = this.GetKey(mth);
            if (await folder.CheckExistsAsync(key) != ExistenceCheckResult.FileExists)
                return default(T);

            var file = await folder.GetFileAsync(key);
            if (file == null)
                return default(T);
            else {
                var str = await file.ReadAllTextAsync();
                return JsonConvert.DeserializeObject<T>(str);
            }
        }

        public async Task Store<T>(BaseMethod<T> mth, T result) {
            if (result == null)
                return;

            var str = JsonConvert.SerializeObject(result);

            var folder = await FileSystem.Current.LocalStorage.CreateFolderAsync("ApiCache", CreationCollisionOption.OpenIfExists);
            var key = this.GetKey(mth);
            var file = await folder.CreateFileAsync(key, CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(str);
        }

        private string GetKey<T>(BaseMethod<T> mth) {
            return $"{mth.GetType().FullName}_{mth.BuildUrl("")}".ToMD5();
        }
    }
}
