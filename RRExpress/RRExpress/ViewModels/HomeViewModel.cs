using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1.Methods;
using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class HomeViewModel : BaseVM {

        public override string Title {
            get {
                return "人人快递";
            }
        }

        //public List<string> AdImgs {
        //    get; set;
        //}

        public BindableCollection<Ad> Ads { get; set; } = new BindableCollection<Ad>();

        public ICommand SendCmd { get; set; }


        private SimpleContainer Container;
        private INavigationService NS;

        public HomeViewModel(SimpleContainer container, INavigationService ns) {
            this.Container = container;
            this.NS = ns;

            this.LoadAds();

            this.SendCmd = new Command(() => this.Send());
        }

        public async void LoadAds() {
            var mth = new GetADs() {
                Type = AdTypes.MobileAdMiddle
            };
            var datas = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            if (!mth.HasError && datas != null) {
                this.Ads.AddRange(datas);
                this.NotifyOfPropertyChange(() => this.Ads);
            }
        }

        public void Send() {
            //var vm = this.Container.GetInstance<SendViewModel>();
            this.NS.NavigateToViewModelAsync<SendStep1ViewModel>();
        }
    }
}
