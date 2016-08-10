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
using Xamarin.Forms;
using Android.Telephony;
using RRExpress.Droid.Services;
using RRExpress.AppCommon.Services;

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