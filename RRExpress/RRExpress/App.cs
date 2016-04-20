using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1;
using RRExpress.Attributes;
using RRExpress.Common;
using RRExpress.ViewModels;
using RRExpress.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RRExpress {
    public partial class App : FormsApplication {

        private SimpleContainer Container = null;

        public App(SimpleContainer container) {
            this.Initialize();

            //加载 App.xaml
            this.InitializeComponent();



            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            this.Container = container;

            //注册 ViewModel
            this.RegistInstances(container);


            #region 初始化 ApiClient
            var asms = new List<Assembly>() {
                typeof(RRExpressV1BaseMethod<>).GetTypeInfo().Assembly
            };
            //TODO 这里使用的是测试环境
            ApiClientOption.Default.UseSandbox = true;
            ApiClient.ApiClient.Init(asms);
            ApiClient.ApiClient.OnMessage += ApiClient_OnMessage;
            #endregion

            this.DisplayRootView<RootView>();
        }

        private async void ApiClient_OnMessage(object sender, ApiClient.MessageArgs e) {
            if (e.ErrorType != null) {
                switch (e.ErrorType.Value) {
                    case ErrorTypes.UnAuth:
                        await this.Container.GetInstance<INavigationService>()
                            .NavigateToViewModelAsync<LoginViewModel>();
                        break;
                    case ErrorTypes.ServiceException:
                        await this.MainPage.DisplayAlert("消息", "报歉，服务暂时无法响应您的请求，请稍候在试", "确定");
                        break;
                    case ErrorTypes.RequestError:
                        await this.MainPage.DisplayAlert("消息", "错误的请求", "确定");
                        break;
                    case ErrorTypes.Network:
                        await this.MainPage.DisplayAlert("消息", "无法连接网络", "确定");
                        break;
                    default:
                        await this.MainPage.DisplayAlert("消息", e.Message, "确定");
                        break;
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
        /// 注册ViewModel
        /// </summary>
        /// <param name="_container"></param>
        private void RegistInstances(SimpleContainer _container) {
            var types = this.GetType().GetTypeInfo().Assembly.DefinedTypes
                .Select(t => {
                    var attr = t.GetCustomAttribute<RegistAttribute>();
                    return new {
                        T = t,
                        Mode = attr?.Mode,
                        TargetType = attr?.ForType
                    };
                })
                .Where(o => o.Mode != null && o.Mode != InstanceMode.None);

            foreach (var t in types) {
                var type = t.T.AsType();
                if (t.Mode == InstanceMode.Singleton) {
                    _container.RegisterSingleton(t.TargetType ?? type, null, type);
                }
                else if (t.Mode == InstanceMode.PreRequest) {
                    _container.RegisterPerRequest(t.TargetType ?? type, null, type);
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
