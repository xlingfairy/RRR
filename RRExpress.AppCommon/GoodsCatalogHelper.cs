using Newtonsoft.Json;
using RRExpress.AppCommon.Models;
using RRExpress.Common;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.AppCommon {
    public static class GoodsCatalogHelper {

        private static readonly IEnumerable<GoodsCategoryTreeNode> Cats = null;

        public static async Task<IEnumerable<GoodsCategoryTreeNode>> GetAll() {
            if (Cats == null) {
                var t = await Task.Run(() => {
                    var assembly = typeof(GoodsCatalogHelper).GetTypeInfo().Assembly;
                    //var res = assembly.GetManifestResourceNames();

                    //格式： 包名.文件名 , 该文件必须是嵌入的资源
                    using (var stream = assembly.GetManifestResourceStream("RRExpress.AppCommon.Cats.json"))
                    using (var reader = new System.IO.StreamReader(stream)) {
                        var text = reader.ReadToEnd();
                        var datas = JsonConvert.DeserializeObject<IEnumerable<GoodsCategory>>(text);
                        return TreeNodeHelper.BuildTree<GoodsCategory, GoodsCategoryTreeNode, int>(datas, p => p.PID, p => p.ID, 0);
                    }
                });
                return t;
            } else
                return Cats;
        }

    }
}
