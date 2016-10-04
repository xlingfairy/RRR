using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1.Methods;
using RRExpress.AppCommon.Attributes;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Express.ViewModels {

    /// <summary>
    /// 取货列表页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class PickupViewModel : ListBase {

        public override string Title {
            get {
                return "收货";
            }
        }

        public ICommand PickupItCmd { get; set; }

        public PickupViewModel(INavigationService ns) {
            this.PickupItCmd = new Command((o) => {
                ns.For<PickupConfirmViewModel>()
                    .WithParam(p => p.Data, (Order)o)
                    .Navigate();
            });
        }

        protected override async Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var mth = new GetMyOrders() {
                Page = page,
                Status = OrderStatus.Geted,
                AsSender = true
            };

            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<object>>(mth.HasError, datas);
        }
    }
}
