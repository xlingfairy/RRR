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

[assembly: Dependency(typeof(ToasImpl))]
namespace AsNum.XFControls.Droid.Services {
    public class ToasImpl : IToast {

        public void Show(string msg, bool longShow = false) {
            var toast = Toast.MakeText(Forms.Context, msg, longShow ? ToastLength.Long : ToastLength.Short);
            //var toast = new Toast(Forms.Context);
            //toast.View = new TextView(Forms.Context) {
            //    Text = msg,
            //    TextSize = 10
            //};
            toast.Show();
        }

    }
}