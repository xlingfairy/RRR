using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Caliburn.Micro;

namespace RRExpress.Droid {
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
        }
    }
}

