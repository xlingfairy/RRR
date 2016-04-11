using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using RRExpress.Common;
using RRExpress.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RRExpress {
    public partial class App : FormsApplication {

        private SimpleContainer Container = null;

        public App(SimpleContainer container) {
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            this.Container = container;

            //注册 ViewModel
            this.RegistInstances(container);


            this.Initialize();

            //加载 App.xaml
            this.InitializeComponent();

            //初始化 ApiClient
            ApiClient.ApiClient.Init();
            ApiClient.ApiClient.OnMessage += ApiClient_OnMessage;

            this.DisplayRootView<RootView>();
        }

        private void ApiClient_OnMessage(object sender, ApiClient.MessageArgs e) {
            if (e.ErrorType != null)
                switch (e.ErrorType.Value) {
                    case ErrorTypes.UnAuth:
                        break;
                    case ErrorTypes.ServiceException:
                        break;
                    case ErrorTypes.RequestError:
                        break;
                    default:
                        break;
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
                } else if (t.Mode == InstanceMode.PreRequest) {
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
