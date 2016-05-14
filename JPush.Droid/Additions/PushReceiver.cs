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

namespace CN.Jpush.Android.Service {

    [BroadcastReceiver(Name = "cn.jpush.android.service.PushReceiver", Enabled = true)]
    [IntentFilter(new string[]{
        "cn.jpush.android.intent.NOTIFICATION_RECEIVED_PROXY"
    },
    Categories = new string[] { Defines.APP_ID },
    Priority = 1000)]
    [IntentFilter(new string[] { 
        "android.intent.action.USER_PRESENT" ,
        "android.net.conn.CONNECTIVITY_CHANGE"
    })]
    [IntentFilter(new string[] { 
        "android.intent.action.PACKAGE_ADDED",
        "android.intent.action.PACKAGE_REMOVED"
    }, DataScheme = "package")]
    public partial class PushReceiver {
    }
}