using RRExpress.AppCommon;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1.Methods;
using RRExpress.AppCommon.Attributes;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 我的发单列表页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class MyOrdersViewModel : ListBase {
        public override string Title {
            get {
                return "我的发单";
            }
        }

        public ICommand ShowInfoCmd { get; }

        protected override async Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var mth = new GetMyOrders() {
                Page = page,
                AsCreator = true
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<object>>(mth.HasError, datas);
        }

        protected override void OnActivate() {
            base.OnActivate();

            this.SelectCommand.Execute(null);
        }

        public MyOrdersViewModel(INavigationService ns) {
            this.ShowInfoCmd = new Command((o) => {
                var order = (Order)o;
                ns.For<MyOrderInfoViewModel>()
                    .WithParam(p => p.Data, order)
                    .Navigate();
            });
        }
    }
}
