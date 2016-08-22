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
using AsNum.XFControls.Services;
using Xamarin.Forms;
using AsNum.XFControls.Droid.Services;
using System.Threading.Tasks;

[assembly: Dependency(typeof(ToasImpl))]
namespace AsNum.XFControls.Droid.Services {
    public class ToasImpl : IToast {

        public void Show(string msg, bool longShow = false) {
            try {
                //Looper.PrepareMainLooper();
                //var toast = Toast.MakeText(Forms.Context, msg, longShow ? ToastLength.Long : ToastLength.Short);
                //toast.Show();
                //Looper.Loop();
                Device.BeginInvokeOnMainThread(() => {
                    var toast = Toast.MakeText(Forms.Context, msg, longShow ? ToastLength.Long : ToastLength.Short);
                    toast.Show();
                });
            } catch (Exception e) {

            }
        }

    }
}