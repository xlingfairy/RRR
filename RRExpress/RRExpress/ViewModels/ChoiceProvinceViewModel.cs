using RRExpress.AppCommon;
using Rg.Plugins.Popup.Services;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ChoiceProvinceViewModel : BaseVM {
        public override string Title {
            get {
                return "选择省份";
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
                this.ChoiceCity(value);
            }
        }

        private ChoiceCityViewModel CityVM = new ChoiceCityViewModel();

        public ChoiceProvinceViewModel() {
            Task.Run(async () => {
                this.Datas = await RegionHelper.GetAll();
                this.NotifyOfPropertyChange(() => this.Datas);
            });
        }

        private async void ChoiceCity(Region province) {
            if (province != null) {
                this.CityVM.Update(province.Children);
                await PopupNavigation.PopAsync();
                await PopupHelper.PopupAsync(this.CityVM);
            }
        }
    }
}
