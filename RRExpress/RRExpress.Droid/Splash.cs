
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace RRExpress.Droid {
    [Activity(Label = "»À»ÀøÏµ›", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true, Theme = "@style/Theme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashScreen : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            var intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
            this.Finish();
        }
    }
}