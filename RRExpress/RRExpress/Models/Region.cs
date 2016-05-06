using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace RRExpress.Models {

    /// <summary>
    /// 区域
    /// </summary>
    public class Region {

        public static readonly IEnumerable<Region> Regions = null;

        public string AreaName { get; set; }

        public decimal Lng { get; set; }

        public decimal Lat { get; set; }

        public IEnumerable<Region> Children { get; set; }

        public static async Task<IEnumerable<Region>> GetAll() {
            if (Regions == null) {
                var t = await Task.Run(() => {
                    var assembly = typeof(Region).GetTypeInfo().Assembly;
                    //var res = assembly.GetManifestResourceNames();
                    
                    //格式： 包名.文件名 , 该文件必须是嵌入的资源
                    using (var stream = assembly.GetManifestResourceStream("RRExpress.Region.json"))
                    using (var reader = new System.IO.StreamReader(stream)) {
                        var text = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<IEnumerable<Region>>(text);
                    }
                });
                return t;
            } else
                return Regions;
        }

        //不使用静态构造，因为数据源比较大，导致第一次使用它的页面打开速度慢
        //static Region() {
        //    Regions = GetAll();
        //}
    }
}
