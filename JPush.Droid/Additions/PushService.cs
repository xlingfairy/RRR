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

    [Service(Name = "cn.jpush.android.service.PushService", Enabled = true, Exported = false)]
    [IntentFilter(new string[] { 
        "cn.jpush.android.intent.REGISTER",
        "cn.jpush.android.intent.REPORT",
        "cn.jpush.android.intent.PushService",
        "cn.jpush.android.intent.PUSH_TIME"
    })]
    public partial class PushService {

    }
}