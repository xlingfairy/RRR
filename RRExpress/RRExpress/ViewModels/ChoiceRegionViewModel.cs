using RRExpress.Api.V1.Methods;
using RRExpress.Attributes;
using RRExpress.Models;
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
                this.Notify();
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
                this.Notify();
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
                this.Notify();
            }
        }

        public Region Town { get; set; }

        public Region Village { get; set; }
        #endregion


        private string _detailAddress;
        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress {
            get {
                return this._detailAddress;
            }
            set {
                this._detailAddress = value;
                this.Notify();
            }
        }





        private ICommand ShowVillageCmd { get; }




        public ChoiceRegionViewModel() {
            Task.Run(async () => {
                this.Datas = await RegionHelper.GetAll();
                this.NotifyOfPropertyChange(() => this.Datas);
            });

            this.ShowVillageCmd = new Command(() => {
                this.GetVillageDatas();
            });
        }


        /// <summary>
        /// 发送消息 
        /// </summary>
        private void Notify() {
            if (this.Village != null)
                MessagingCenter.Send(this, MESSAGE_KEY, new ChoicedRegion() {
                    FullName = $"{this.Province?.AreaName} {this.City?.AreaName} {this.County?.AreaName} {this.Town?.AreaName} {this.Village?.AreaName} {this.DetailAddress}",
                    Region = this.Village,
                    ProvinceName = this.Province.AreaName,
                    CityName = this.City.AreaName,
                    CountyName = this.County.AreaName,
                    TownName = this.Town.AreaName,
                    DetailAddress = this.DetailAddress
                });

            this.CanShowVillage = false;
            this.NotifyOfPropertyChange(() => this.CanShowVillage);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        public void Update(ChoicedRegion data) {
            if (data != null) {
                this.Province = this.Datas.FirstOrDefault(d => d.AreaName.Equals(data.ProvinceName, StringComparison.OrdinalIgnoreCase));
                if (this.Province != null) {
                    this.City = this.Province.Children.FirstOrDefault(d => d.AreaName.Equals(data.CityName, StringComparison.OrdinalIgnoreCase));
                    if (this.City != null) {
                        this.County = this.City.Children.FirstOrDefault(d => d.AreaName.Equals(data.Region.AreaName));
                    }
                }
                this.DetailAddress = data.DetailAddress;
                this.NotifyOfPropertyChange(() => this.Province);
                this.NotifyOfPropertyChange(() => this.City);
                this.NotifyOfPropertyChange(() => this.County);
                this.NotifyOfPropertyChange(() => this.DetailAddress);
            }
        }


        private async void GetVillageDatas() {
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

            if (data != null && this.VillageDatas.Count() > 0) {
                this.CanShowVillage = true;
                this.VillageDatas = data;
                this.NotifyOfPropertyChange(() => this.VillageDatas);
                this.NotifyOfPropertyChange(() => this.CanShowVillage);
            }
        }
    }
}
