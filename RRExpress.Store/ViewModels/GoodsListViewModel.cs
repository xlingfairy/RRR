using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class GoodsListViewModel : ListBase {
        public override string Title {
            get {
                return "商品列表";
            }
        }


        protected override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var datas = Enumerable.Range(page * 20, 20)
                .Select(i => new GoodsInfo() {
                    Name = $"本地红薯{i}",
                    Price = 0.8M,
                    OrgPrice = 1.0M,
                    Thumbnail = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
                }
            );
            return Task.FromResult(new Tuple<bool, IEnumerable<object>>(false, datas));
        }

        public async Task FirstLoad() {
            await this.LoadData();
        }
    }
}
