using Newtonsoft.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace RRExpress.AppCommon {
    public static class ResJsonReader {

        public static Task<T> GetAll<T>(Assembly asm, string catsResFile) {
            return Task.Run(() => {
                //格式： 包名.文件名 , 该文件必须是嵌入的资源
                using (var stream = asm.GetManifestResourceStream(catsResFile))
                using (var reader = new System.IO.StreamReader(stream)) {
                    //return TreeNodeHelper.BuildTree<GoodsCategory, GoodsCategoryTreeNode, int>(datas, p => p.PID, p => p.ID, 0);
                    var text = reader.ReadToEnd();
                    var datas = JsonConvert.DeserializeObject<T>(text);
                    return datas;
                }
            });
        }

    }
}
