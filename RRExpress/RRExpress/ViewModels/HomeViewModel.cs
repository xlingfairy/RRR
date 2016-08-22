using RRExpress.AppCommon;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Plugin.Geolocator;
using RRExpress.AppCommon.Attributes;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using RRExpress.Express.ViewModels;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 第一页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class HomeViewModel : BaseVM {

        public override string Title {
            get {
                return "人人快递";
            }
        }

        public List<string> AdImgs {
            get; set;
        }

        public ICommand SendCmd { get; }
        public ICommand SellerCmd { get; }
        public ICommand StoreCmd { get; }


        private SimpleContainer Container;
        private INavigationService NS;

        public HomeViewModel(SimpleContainer container, INavigationService ns) {
            this.Container = container;
            this.NS = ns;

            this.AdImgs = new List<string>() {
                "http://www.jiaojianli.com/wp-content/uploads/2013/12/banner_send.jpg",
                "http://img1.100ye.com/img2/4/1230/627/10845627/msgpic/62872955.jpg",
                "http://static.3158.com/im/image/20140820/20140820022157_32140.jpg"
            };

            this.SendCmd = new Command(async () => {
                await this.NS.NavigateToViewModelAsync<SendStep1ViewModel>();
            });

            this.SellerCmd = new Command(async () => {
                await this.NS.NavigateToViewModelAsync<Seller.ViewModels.AddGoodsViewModel>();
            });

            this.StoreCmd = new Command(async () => {
                await this.NS.NavigateToViewModelAsync<Store.ViewModels.RootViewModel>();
            });
        }

        private async void GetLocation() {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100; //100 is new default
            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
        }
    }
}
