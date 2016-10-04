using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1.Methods;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MessageListViewModel : ListBase {
        public override string Title {
            get {
                return "消息中心";
            }
        }


        private RRExpress.Service.Entity.Message _selected = null;
        public RRExpress.Service.Entity.Message Selected {
            get {
                return this._selected;
            }
            set {
                this._selected = value;
                this.ShowMessage(value);
            }
        }

        private INavigationService NS = null;

        public MessageListViewModel(INavigationService ns) {
            this.NS = ns;
        }

        protected async override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var mth = new GetMessageList() {
                Page = page
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            return new Tuple<bool, IEnumerable<object>>(mth.HasError, datas);
        }


        protected async override void OnActivate() {
            base.OnActivate();

            if (this.Datas == null || this.Datas.Count == 0) {
                await Task.Delay(500)
                    .ContinueWith(async t => {
                        await this.LoadData(true);
                    });
            }
        }

        private void ShowMessage(RRExpress.Service.Entity.Message msg) {
            if (msg == null)
                return;

            msg.IsReaded = true;

            this.NS.For<MessageViewModel>()
                .WithParam(p => p.ID, msg.MessageID)
                .Navigate();
        }
    }
}
