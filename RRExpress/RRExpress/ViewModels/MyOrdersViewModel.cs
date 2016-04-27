using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRExpress.Service.Entity;
using RRExpress.Api.V1.Methods;
using RRExpress.Attributes;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyOrdersViewModel : OrderList {
        public override string Title {
            get {
                return "我的发单";
            }
        }

        public override async Task<Tuple<bool, IEnumerable<Order>>> GetDatas(int page) {
            var mth = new GetMyOrders() {
                Page = page,
                AsCreator = true
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<Order>>(mth.HasError, datas);
        }

        protected override void OnActivate() {
            base.OnActivate();

            this.SelectCommand.Execute(null);
        }
    }
}
