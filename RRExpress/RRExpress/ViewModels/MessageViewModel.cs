using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1.Methods;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MessageViewModel : BaseVM {
        public override string Title {
            get {
                return "消息";
            }
        }

        public int ID { get; set; }

        public RRExpress.Service.Entity.Message Data { get; set; }

        protected override void OnActivate() {
            base.OnActivate();

            this.LoadData();
        }

        private async void LoadData() {
            this.IsBusy = true;

            var mth = new GetMessage() {
                ID = this.ID
            };

            this.Data = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            this.NotifyOfPropertyChange(() => this.Data);

            this.IsBusy = false;
        }
    }
}
