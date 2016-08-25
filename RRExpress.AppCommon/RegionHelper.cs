using Newtonsoft.Json;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace RRExpress.AppCommon {

    /// <summary>
    /// 区域
    /// </summary>
    public static class RegionHelper {

        private static IEnumerable<Region> Regions = null;

        public static async Task<IEnumerable<Region>> GetAll() {
            if (Regions == null) {
                Regions = await ResJsonReader.GetAll<IEnumerable<Region>>(typeof(RegionHelper).GetTypeInfo().Assembly, "RRExpress.AppCommon.Region.json");
            }
            return Regions;
        }

        //不使用静态构造，因为数据源比较大，导致第一次使用它的页面打开速度慢
        //static Region() {
        //    Regions = GetAll();
        //}
    }
}
