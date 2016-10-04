using Android.App;
using Android.Content.PM;
using Android.OS;
using Caliburn.Micro;
using CN.Jpush.Android.Api;
using Xamarin.Forms.Platform.Android;

namespace RRExpress.Droid {

    [Activity(Label = "RRExpress", Theme = "@style/MyTheme", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            // https://github.com/Caliburn-Micro/Caliburn.Micro/issues/298
            //this.LoadApplication(new App(IoC.Get<SimpleContainer>()));
            this.LoadApplication(IoC.Get<App>());

            //TODO 调试模式, 发布时请改为 false
            JPushInterface.SetDebugMode(true);
            JPushInterface.Init(this);
        }
    }
}

