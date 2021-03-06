﻿using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Newtonsoft.Json;
using RRExpress.Api.V1;
using RRExpress.AppCommon;
using RRExpress.Common;
using RRExpress.ViewModels;
using RRExpress.Views;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RRExpress {
    public partial class App : FormsApplication {

        /// <summary>
        /// 推送来的未读订单数
        /// </summary>
        public static readonly string PUSH_UNREAD_ORDER_COUNT = "PUSH_UNREAD_ORDER_COUNT";


        /// <summary>
        /// 在 IOS / Android 项目中使用，用于从其它DLL中加载视图
        /// </summary>
        public static IEnumerable<Assembly> UsedAssemblies {
            get {
                return new List<Assembly>() {
                    typeof(App).GetTypeInfo().Assembly,
                    typeof(Express.ViewModels.AddPriceViewModel).GetTypeInfo().Assembly,
                    typeof(Seller.ViewModels.RegistViewModel).GetTypeInfo().Assembly,
                    typeof(Store.Settings.MyOrders).GetTypeInfo().Assembly
                };
            }
        }


        private SimpleContainer Container = null;

        public App(SimpleContainer container) {
            this.Initialize();

            //加载 App.xaml
            this.InitializeComponent();

            //线程异常处理
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            this.Container = container;

            //注册 ViewModel
            container.RegistInstances(UsedAssemblies);

            //修改默认提供器
            ApiClientCacheProvider.Default = new ApiClientCacheStore();
            BearTokenProvider.Default = new BearerTokenStore();

            //初始化 ApiClient, 如正式发布，请删除参数 true
            this.InitApiClient(true);

            //Task.Run(async () => {
            //    await this.ShowRootView();
            //});
            this.ShowRootView();
        }


        #region apiClient
        /// <summary>
        /// 
        /// </summary>
        /// <param name="useSandbox">是否使用测试环境</param>
        private void InitApiClient(bool useSandbox = false) {
            //需要从哪些程序集中导入 MEF
            //手机版 APP 不方便获取所有加载的程序集，也不方便获取所有的 Dll
            //所在此处，需要手工指定
            var asms = new List<Assembly>() {
                typeof(RRExpressV1BaseMethod<>).GetTypeInfo().Assembly,
                typeof(App).GetTypeInfo().Assembly
            };


            ApiClientOption.Default.UseSandbox = useSandbox;
            ApiClient.ApiClient.Init(asms);
            ApiClient.ApiClient.OnMessage += ApiClient_OnMessage;
        }

        private void ApiClient_OnMessage(object sender, ApiClient.MessageArgs e) {
            if (e.ErrorType != null) {
                switch (e.ErrorType.Value) {
                    case ErrorTypes.UnAuth:
                        Device.BeginInvokeOnMainThread(async () => {
                            await this.Container.GetInstance<INavigationService>()
                                .NavigateToViewModelAsync<LoginViewModel>();
                        });
                        break;
                    case ErrorTypes.ServiceException:
                        this.ShowMessage("消息", "报歉，服务暂时无法响应您的请求，请稍候在试", "确定");
                        break;
                    case ErrorTypes.RequestError:
                        this.ShowMessage("消息", "错误的请求", "确定");
                        break;
                    case ErrorTypes.Network:
                        if (!NetworkInterface.GetIsNetworkAvailable()) {
                            this.ShowMessage("消息", "似乎无法连接网络", "确定");
                        }
                        else {
                            this.ShowMessage("消息", "我们暂时无法为您提供服务,请稍候重试", "确定");
                        }
                        break;
                    default:
                        this.ShowMessage("消息", e.Message, "确定");
                        break;
                }
            }
        }

        private void ShowMessage(string title, string msg, string cancelBtn) {
            Device.BeginInvokeOnMainThread(async () => {
                await this.MainPage.DisplayAlert(title, msg, cancelBtn);
            });
        }
        #endregion

        public async void ShowRootView() {
            //如果保存的登陆票据无效,则重新登陆
            if (!BearTokenProvider.Default.IsValid) {
                this.DisplayRootView<LoginView>();
            }
            else {
                //如果保存的登陆票据有效(不是通过登陆页面进来的)
                if (this.RootNavigationPage == null) {
                    this.DisplayRootView<RootView>();
                }
                else {
                    //这里是通过登陆页面跳转进来的
                    await this.Container.GetInstance<INavigationService>()
                                .NavigateToViewModelAsync<RootViewModel>();
                    var nav = this.MainPage.Navigation;
                    var fp = nav.NavigationStack.First();
                    if (fp is LoginView) {
                        //移除位于根上的 LoginView , 避免用户通过返回键回退到登陆页
                        nav.RemovePage(fp);
                    }
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
            System.Diagnostics.Debug.WriteLine(e.Exception.StackTrace);
            e.SetObserved();
        }




        /// <summary>
        /// 转发推送的消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="extraJson"></param>
        public void RelyPushMessage(string msg, string extraJson) {
            if (!string.IsNullOrWhiteSpace(extraJson)) {
                try {
                    var o = new { Type = "" };
                    o = JsonConvert.DeserializeAnonymousType(extraJson, o);
                    if (o.Type.Equals("UOC")) {
                        //未读订单数
                        var unReadOrderCount = msg.ToIntOrNull();
                        //发布消息, MyViewModel 会订阅该消息
                        MessagingCenter.Send(this, PUSH_UNREAD_ORDER_COUNT, unReadOrderCount);
                    }
                }
                catch {

                }
            }
        }


        protected override void PrepareViewFirst(NavigationPage navigationPage) {
            this.Container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }



        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps

        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
