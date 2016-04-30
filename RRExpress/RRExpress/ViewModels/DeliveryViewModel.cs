using AsNum.XFControls;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RRExpress.Service.Entity;
using RRExpress.Api.V1.Methods;
using Caliburn.Micro.Xamarin.Forms;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 待送货订单列表
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class DeliveryViewModel : ListBase {

        public override string Title {
            get {
                return "送货";
            }
        }

        public ICommand ShowConfirmCmd { get; }

        public DeliveryViewModel(INavigationService ns) {
            this.ShowConfirmCmd = new Command(o => {
                var order = (Order)o;
                ns.For<DeliveryConfirmViewModel>()
                .WithParam(p => p.Data, order)
                .Navigate();
            });
        }

        protected override async Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var mth = new GetMyOrders() {
                Page = page,
                Status = OrderStatus.Picked,
                AsSender = true
            };

            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<object>>(mth.HasError, datas);
        }
    }
}
