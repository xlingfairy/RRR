using Android.Content;
using Android.Telephony;
using RRExpress.AppCommon.Services;
using RRExpress.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceImpl))]
namespace RRExpress.Droid.Services {
    public class DeviceImpl : IDevice {

        public string GetDeviceID() {
            using (var manager = (TelephonyManager)Forms.Context.GetSystemService(Context.TelephonyService)) {
                return manager.DeviceId;
            }
        }

        public string GetPhoneNumber() {
            using (var manager = (TelephonyManager)Forms.Context.GetSystemService(Context.TelephonyService)) {
                return manager.Line1Number;
            }
        }
    }
}