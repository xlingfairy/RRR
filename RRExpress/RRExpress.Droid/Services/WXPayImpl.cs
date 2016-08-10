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
using System.Threading.Tasks;
using RRExpress.Droid.Services;
using RRExpress.AppCommon.Services;

[assembly: Dependency(typeof(WXPayImpl))]
namespace RRExpress.Droid.Services {
    public class WXPayImpl : IWXPay {
        public async Task Pay(string title, decimal fee) {
            var wp = new WXPay.WXPay(Forms.Context);
            var pid = await wp.GoPrePayId(title, fee);
            var req = wp.GenPayReq(pid);
            wp.SendPayReq(req);
        }
    }
}