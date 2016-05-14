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
using Android.Content.PM;

namespace CN.Jpush.Android.UI {

    [Activity(Name = "cn.jpush.android.ui.PushActivity",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden,
        Exported = false)]
    [IntentFilter(new string[] { 
        "cn.jpush.android.ui.PushActivity"
    }, Categories = new string[]{
        "android.intent.category.DEFAULT",
        Defines.APP_ID
    })]
    public partial class PushActivity {

        public void TTT() {
        }

    }
}