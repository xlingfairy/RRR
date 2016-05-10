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
using RRExpress.Services;

namespace RRExpress.Droid.Services {
    public class WXPayImpl : IWXPay {
        public void Pay(string title, decimal fee) {
            throw new NotImplementedException();
        }
    }
}