using RRExpress.Attributes;
using RRExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
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

        private void Notify() {
            if (this.County != null)
                MessagingCenter.Send(this, MESSAGE_KEY, new ChoicedRegion() {
                    FullName = $"{this.Province?.AreaName} {this.City?.AreaName} {this.County?.AreaName}",
                    Region = this.County
                });
        }

        //public ChoiceRegionViewModel() {
        //    //this.PropertyChanged += ChoiceRegionViewModel_PropertyChanged;
        //}

        //private void ChoiceRegionViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
        //    switch (e.PropertyName) {
        //        case nameof(this.Province):
        //        case nameof(this.City):
        //        case nameof(this.County):
        //            if (this.County != null)
        //                MessagingCenter.Send(this, MESSAGE_KEY, new ChoicedRegion() {
        //                    FullName = $"{this.Province?.AreaName} {this.City?.AreaName} {this.County?.AreaName}",
        //                    Region = this.County
        //                });
        //            break;
        //    }
        //}
    }
}
