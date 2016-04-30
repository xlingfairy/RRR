using AsNum.XFControls;
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
    /// 我的发单,详情状态子页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class MyOrderStatusViewModel : ListBase {
        public override string Title {
            get {
                return "订单状态";
            }
        }

        private Order _data = null;
        public Order Data {
            get {
                return this._data;
            }
            set {
                this._data = value;
                this.NotifyOfPropertyChange(() => this.Data);
                this.Datas.Clear();
                Task.Delay(500).ContinueWith(async t => {
                    await this.LoadData(true);
                });

            }
        }


        protected async override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var mth = new GetOrderEvents() {
                OrderNO = this.Data.OrderNO
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<object>>(mth.HasError, datas);
        }
    }
}
