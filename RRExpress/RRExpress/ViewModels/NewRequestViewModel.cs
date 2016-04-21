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
    public class NewRequestViewModel : BaseVM, ISelectable {

        public override string Title {
            get {
                return "接单";
            }
        }

        #region ISelectable
        public bool IsSelected {
            get; set;
        }

        public ICommand SelectCommand {
            get; set;
        }
        #endregion

        public ICommand RefreshCmd { get; }

        public ICommand LoadMoreCmd { get; }

        public ICommand GetItCmd { get; }

        public BindableCollection<NewRequest> Datas {
            get; set;
        }



        private int CurrPage = 0;

        public NewRequestViewModel(INavigationService ns) {
            this.Datas = new BindableCollection<NewRequest>();

            this.LoadMoreCmd = new Command(async () => {
                await this.LoadData();
            });

            this.RefreshCmd = new Command(async () => {
                await this.LoadData(true);
            });

            this.GetItCmd = new Command((o) => {
                var newRequest = (NewRequest)o;
                ns.For<OrderDetailViewModel>()
                .WithParam(m => m.Data, newRequest)
                .Navigate();
            });

            Task.Delay(1000)
                .ContinueWith(t => this.LoadData(true));
        }

        //TODO Not Fire
        //protected async override void OnActivate() {
        //    base.OnActivate();
        //    await this.LoadData();
        //}

        private async Task LoadData(bool isReload = false) {
            //if (this.IsBusy) {
            //    //ListView.IsRefreshing 绑定到这个属性上, 造成双向绑定,所以, 不能用它作为判断
            //    //return;
            //}

            this.IsBusy = true;
            var mth = new GetNewRequests() {
                Page = isReload ? 0 : this.CurrPage + 1
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            if (!mth.HasError && datas != null && datas.Count() > 0) {
                if (isReload)
                    this.Datas.Clear();

                this.CurrPage = mth.Page;
                this.Datas.AddRange(datas);
            }
            this.IsBusy = false;
        }
    }
}
