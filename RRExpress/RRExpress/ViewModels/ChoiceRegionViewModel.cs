using RRExpress.Attributes;
using RRExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public ChoiceRegionViewModel() {
            Task.Run(async () => {
                this.Datas = await Region.GetAll();
                this.NotifyOfPropertyChange(() => this.Datas);
            });
        }


        /// <summary>
        /// 发送消息 
        /// </summary>
        private void Notify() {
            if (this.County != null)
                MessagingCenter.Send(this, MESSAGE_KEY, new ChoicedRegion() {
                    FullName = $"{this.Province?.AreaName} {this.City?.AreaName} {this.County?.AreaName} {this.DetailAddress}",
                    Region = this.County,
                    ProvinceName = this.Province.AreaName,
                    CityName = this.City.AreaName,
                    DetailAddress = this.DetailAddress
                });
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
    }
}
