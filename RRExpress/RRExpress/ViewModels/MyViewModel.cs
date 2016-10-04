using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
using RRExpress.Express.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyViewModel : BaseVM {
        public override string Title {
            get {
                return "我的信息";
            }
        }

        public ICommand ShowEditCmd { get; }

        //public ICommand ShowJoinCmd { get; }

        //public ICommand ShowMyOrdersCmd { get; }

        public ICommand ShowMyPointCmd { get; }

        //public ICommand LogoutCmd { get; }


        //public Dictionary<string, List<ISettingItem>> Settings { get; }
        public IEnumerable<Grouped<ISettingItem>> Datas { get; set; }


        public int? UnReadOrderCount { get; set; }
        public bool IsShowUnReadOrderCount { get; set; }


        private SimpleContainer Container;
        private INavigationService NS;


        public MyViewModel(SimpleContainer container, INavigationService ns) {

            this.Container = container;
            this.NS = ns;

            var settingItems = container.GetAllInstances<ISettingItem>();
            this.Datas = settingItems.ToGroup(s => s.CustomCatlog, s => s.Order);

            //this.Settings = settingItems.Where(s => s.CanUse)
            //    .GroupBy(s => {
            //        return s.Catlog.HasValue ? s.Catlog.ToString() : s.CustomCatlog;
            //    })
            //    .ToDictionary(g => g.Key, g => g.OrderBy(s => s.Order).ToList());


            this.ShowEditCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<EditMyInfoViewModel>();
            });

            //this.ShowJoinCmd = new Command(async () => {
            //    await ns.NavigateToViewModelAsync<JoinWizardViewModel>();
            //});

            //this.ShowMyOrdersCmd = new Command(async () => {
            //    await ns.NavigateToViewModelAsync<MyOrdersViewModel>();
            //    this.UnReadOrderCount = 0;
            //    this.IsShowUnReadOrderCount = false;
            //    this.NotifyOfPropertyChange(() => this.UnReadOrderCount);
            //    this.NotifyOfPropertyChange(() => this.IsShowUnReadOrderCount);
            //});

            this.ShowMyPointCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<MyPointsViewModel>();
            });

            //this.LogoutCmd = new Command(async () => {
            //    PropertiesHelper.Remove("UserToken");
            //    await PropertiesHelper.Save();
            //    await ns.NavigateToViewModelAsync<LoginViewModel>();
            //    var nav = App.Current.MainPage.Navigation;
            //    var fp = nav.NavigationStack.First();
            //    if (!(fp is LoginView)) {
            //        nav.RemovePage(fp);
            //    }
            //});

            ////订阅由 App 转发的 推送消息:未读订单数
            //MessagingCenter.Subscribe<App, int?>(this, App.PUSH_UNREAD_ORDER_COUNT, (sender, i) => {
            //    this.UnReadOrderCount = i;
            //    this.IsShowUnReadOrderCount = (this.UnReadOrderCount.HasValue && this.UnReadOrderCount > 0);
            //    this.NotifyOfPropertyChange(() => this.UnReadOrderCount);
            //    this.NotifyOfPropertyChange(() => this.IsShowUnReadOrderCount);
            //});
        }

        public async Task ExecuteSetting(ISettingItem o) {
            if (o != null)
                try {
                    await o.Execute(this.Container, this.NS);
                }
                catch (Exception e) {
                    throw e;
                }
        }
    }
}
