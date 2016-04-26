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

    [Regist(InstanceMode.Singleton)]
    public class DeliveryViewModel : OrderList {

        public override string Title {
            get {
                return "送货";
            }
        }

        public ICommand ShowConfirmCmd { get; }

        public DeliveryViewModel(INavigationService ns) {
            this.ShowConfirmCmd = new Command(o => {

            });
        }

        public override async Task<Tuple<bool, IEnumerable<Order>>> GetDatas(int page) {
            var mth = new GetMyOrders() {
                Page = page,
                Status = OrderStatus.Picked
            };

            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<Order>>(mth.HasError, datas);
        }
    }
}
