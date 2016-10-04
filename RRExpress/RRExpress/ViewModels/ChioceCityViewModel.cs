using Rg.Plugins.Popup.Services;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Service.Entity;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ChoiceCityViewModel : BaseVM {

        public static readonly string MESSAGE_KEY = "CHOICE_CITY";

        public override string Title {
            get {
                return "选择城市";
            }
        }

        public IEnumerable<Region> Datas { get; private set; }


        private Region _selected = null;
        public Region Selected {
            get {
                return this._selected;
            }
            set {
                this._selected = value;
                this.ChangeCity(value);
            }
        }

        public void Update(IEnumerable<Region> datas) {
            this.Datas = datas;
            this.NotifyOfPropertyChange(() => this.Datas);
        }

        private void ChangeCity(Region region) {
            if (region != null) {
                MessagingCenter.Send(this, MESSAGE_KEY, region.AreaName);
                PopupNavigation.PopAsync();
            }
        }
    }
}
