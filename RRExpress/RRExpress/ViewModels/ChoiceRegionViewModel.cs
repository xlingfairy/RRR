using RRExpress.AppCommon;
using Rg.Plugins.Popup.Services;
using RRExpress.Api.V1.Methods;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ChoiceRegionViewModel : BaseVM {

        public static readonly string MESSAGE_KEY = "CHOICE_REGION";

        public override string Title {
            get {
                return "区域选择";
            }
        }


        public IEnumerable<Region> Datas { get; private set; }

        public IEnumerable<Region> VillageDatas { get; private set; }

        public bool CanShowVillage { get; set; }

        #region
        private Region _privince = null;
        /// <summary>
        /// 省
        /// </summary>
        public Region Province {
            get {
                return this._privince;
            }
            set {
                this._privince = value;
                this.NotifyNeedReChoiceVillage();
            }
        }

        private Region _city;
        /// <summary>
        /// 城市
        /// </summary>
        public Region City {
            get {
                return this._city;
            }
            set {
                this._city = value;
                this.NotifyNeedReChoiceVillage();
            }
        }


        private Region _county;
        /// <summary>
        /// 县/区
        /// </summary>
        public Region County {
            get {
                return this._county;
            }
            set {
                this._county = value;
                this.NotifyNeedReChoiceVillage();
            }
        }

        public Region Town { get; set; }

        public Region Village { get; set; }
        #endregion


        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress {
            get; set;
        }





        public ICommand ShowVillageCmd { get; }
        public ICommand OKCmd { get; }



        public ChoiceRegionViewModel() {
            Task.Run(async () => {
                this.Datas = await RegionHelper.GetAll();
                this.NotifyOfPropertyChange(() => this.Datas);
            });

            this.ShowVillageCmd = new Command(async () => {
                await this.GetVillageDatas();
            });

            this.OKCmd = new Command(async () => {
                if (this.Village != null) {
                    MessagingCenter.Send(this, MESSAGE_KEY, new ChoicedRegion() {
                        FullName = $"{this.Province?.AreaName} {this.City?.AreaName} {this.County?.AreaName} {this.Town?.AreaName} {this.Village?.AreaName} {this.DetailAddress}",
                        Region = this.Village,
                        ProvinceName = this.Province.AreaName,
                        CityName = this.City.AreaName,
                        CountyName = this.County.AreaName,
                        TownName = this.Town.AreaName,
                        DetailAddress = this.DetailAddress
                    });
                    await PopupNavigation.PopAsync();
                }
            });
        }


        private void NotifyNeedReChoiceVillage() {
            this.CanShowVillage = false;
            this.NotifyOfPropertyChange(() => this.CanShowVillage);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        public async void Update(ChoicedRegion data) {
            if (data != null) {
                this.Province = this.Datas.FirstOrDefault(d => d.AreaName.Equals(data.ProvinceName, StringComparison.OrdinalIgnoreCase));
                if (this.Province != null) {
                    this.City = this.Province.Children.FirstOrDefault(d => d.AreaName.Equals(data.CityName, StringComparison.OrdinalIgnoreCase));
                    if (this.City != null) {
                        this.County = this.City.Children.FirstOrDefault(d => d.AreaName.Equals(data.CountyName));
                        //还原乡村级数据
                        await this.GetVillageDatas();
                        this.Town = this.VillageDatas.FirstOrDefault(d => d.AreaName.Equals(data.TownName));
                        this.Village = this.Town.Children.FirstOrDefault(d => d.AreaName.Equals(data.Region.AreaName));
                    }
                }
                this.DetailAddress = data.DetailAddress;

                this.NotifyOfPropertyChange(() => this.Province);
                this.NotifyOfPropertyChange(() => this.City);
                this.NotifyOfPropertyChange(() => this.County);
                this.NotifyOfPropertyChange(() => this.Town);
                this.NotifyOfPropertyChange(() => this.Village);
                this.NotifyOfPropertyChange(() => this.DetailAddress);
            }
        }


        private async Task GetVillageDatas() {
            this.VillageDatas = null;
            this.CanShowVillage = false;
            this.NotifyOfPropertyChange(() => this.VillageDatas);
            this.NotifyOfPropertyChange(() => this.CanShowVillage);

            this.IsBusy = true;

            var mth = new GetVillages() {
                AllowCache = true,
                County = this.County.AreaName,
                City = this.City.AreaName,
                Province = this.Province.AreaName
            };
            var data = await ApiClient.ApiClient.Instance.Value.GetDataFromCache(mth);
            if (data == null) {
                data = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            }

            if (data != null && data.Count() > 0) {
                this.CanShowVillage = true;
                this.VillageDatas = data;
                this.NotifyOfPropertyChange(() => this.VillageDatas);
                this.NotifyOfPropertyChange(() => this.CanShowVillage);
            }

            this.IsBusy = false;
        }
    }
}
