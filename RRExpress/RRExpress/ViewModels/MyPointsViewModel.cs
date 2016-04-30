using RRExpress.Api.V1.Methods;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class MyPointsViewModel : ListBase {
        public override string Title {
            get {
                return "我的积分";
            }
        }

        protected async override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var mth = new GetMyPoint() {
                Page = page
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<object>>(mth.HasError, datas);
        }

        protected async override void OnActivate() {
            base.OnActivate();

            await Task.Delay(500)
                .ContinueWith(async t => {
                    await this.LoadData(true);
                });

        }
    }
}
