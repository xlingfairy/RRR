using AsNum.XFControls;
using Caliburn.Micro;
using RRExpress.Api.V1.Methods;
using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ICommand RefreshCmd { get; set; }

        public ICommand ReloadCmd { get; set; }

        public BindableCollection<NewRequest> Datas {
            get; set;
        }



        private int CurrPage = 0;

        public NewRequestViewModel() {
            this.Datas = new BindableCollection<NewRequest>();

            Task.Delay(1000)
                .ContinueWith(t => this.LoadData());
        }

        //TODO Not Fire
        //protected async override void OnActivate() {
        //    base.OnActivate();
        //    await this.LoadData();
        //}

        private async Task LoadData(bool isReload = false) {
            var mth = new GetNewRequests() {
                Page = isReload ? this.CurrPage + 1 : 0
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            if (!mth.HasError) {
                if (isReload)
                    this.Datas.Clear();

                this.CurrPage = mth.Page;
                this.Datas.AddRange(datas);
            }
        }
    }
}
