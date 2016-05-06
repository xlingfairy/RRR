using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class EvaluationViewModel : BaseVM {
        public override string Title {
            get {
                return "请对发货人评价";
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
            }
        }


        public List<string> CommunicationOptions {
            get;
        } = new List<string>() { "很亲切", "正常", "粗鲁" };
    }
}
