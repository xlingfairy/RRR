using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Caliburn.Micro;
using System.Reflection;
using Java.Lang;
using System.Threading.Tasks;

namespace RRExpress.Droid {
    [Application]
    public class Application : CaliburnApplication {
        private SimpleContainer container;

        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer) {


            //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            
        }

        void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e) {
            //线程取消异常, 不好捕捉,在这里可以处理掉.
            //System.Threading.Tasks.TaskCanceledException: A task was canceled.
            e.Handled = true;
        }

        public override void OnCreate() {
            base.OnCreate();

            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();
            container.Instance(container);

            // https://github.com/Caliburn-Micro/Caliburn.Micro/issues/298
            container.Singleton<App>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies() {
            return new[]
            {
                GetType().Assembly,
                typeof (App).Assembly
            };
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }
    }
}