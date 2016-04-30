using AsNum.XFControls;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1.Methods;
using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 可接订单列表
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class NewOrdersViewModel : OrderList, ISelectable {

        public override string Title {
            get {
                return "接单";
            }
        }

        public ICommand GetItCmd { get; }

        public NewOrdersViewModel(INavigationService ns) {
            this.GetItCmd = new Command((o) => {
                var newRequest = (Order)o;
                ns.For<ConfirmGetOrderViewModel>()
                .WithParam(m => m.Data, newRequest)
                .Navigate();
            });
        }

        public override async Task<Tuple<bool, IEnumerable<Order>>> GetDatas(int page) {
            var mth = new GetNewOrders() {
                Page = page
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<Order>>(mth.HasError, datas);
        }
    }
}
