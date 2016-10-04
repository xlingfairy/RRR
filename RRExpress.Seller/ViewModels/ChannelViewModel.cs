using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;

namespace RRExpress.Seller.ViewModels {
    [Regist(InstanceMode.PreRequest)]
    public class ChannelViewModel : BaseVM {

        public event EventHandler SelectedChanged;

        public override string Title {
            get {
                return "产品所属频道";
            }
        }

        public IEnumerable<string> Datas {
            get;
        } = new List<string>() { "自营超市", "家乡农村", "身边商店" };


        private string _selected;
        public string Selected {
            get {
                return this._selected;
            }
            set {
                this._selected = value;
                this.NotifyOfPropertyChange(() => this.Selected);
            }
        }

    }
}
