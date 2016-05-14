using Android.App;
using Android.Runtime;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Reflection;
using Android.OS;
using Plugin.CurrentActivity;

namespace RRExpress.Droid {
    [Application]
    [MetaData("JPUSH_CHANNEL", Value = "developer-default")]
    [MetaData("JPUSH_APPKEY", Value = "8050b214eadc221a5ad3c161")]
    public class RApplication : CaliburnApplication, Application.IActivityLifecycleCallbacks {
        private SimpleContainer container;

        public RApplication(IntPtr javaReference, JniHandleOwnership transfer)
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
            RegisterActivityLifecycleCallbacks(this);
        }


        public override void OnTerminate() {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
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







        public void OnActivityCreated(Activity activity, Bundle savedInstanceState) {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity) {
            
        }

        public void OnActivityPaused(Activity activity) {
            
        }

        public void OnActivityResumed(Activity activity) {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) {
            
        }

        public void OnActivityStarted(Activity activity) {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity) {

        }
    }
}