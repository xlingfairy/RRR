
using Android.App;
using Android.Content.PM;
using Android.OS;
using Caliburn.Micro;
using CN.Jpush.Android.Api;
using Xamarin.Forms.Platform.Android;

namespace RRExpress.Droid {
    //[IntentFilter( 
    //    new string[] { "android.intent.action.VIEW" }, 
    //    Categories = new string[] { "android.intent.category.DEFAULT" },
    //    DataScheme = "wx3bddc7a533a50999")]

    [Activity(Label = "RRExpress", Theme = "@style/MyTheme", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            // https://github.com/Caliburn-Micro/Caliburn.Micro/issues/298
            //this.LoadApplication(new App(IoC.Get<SimpleContainer>()));
            this.LoadApplication(IoC.Get<App>());

            //PushSettings.EnableDebugMode(this, true);

            //PushManager.StartWork(this, PushConstants.LoginTypeApiKey, "1WWG7rvzGxee67N1mLU4GfNs");
            //if (!PushManager.IsPushEnabled(this))
            //    PushManager.ResumeWork(this);

            JPushInterface.SetDebugMode(true);
            JPushInterface.Init(this);
        }
    }
}

