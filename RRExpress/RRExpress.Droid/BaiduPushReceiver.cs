using Android.App;
using Android.Content;
using Com.Baidu.Android.Pushservice;
using System.Collections.Generic;

namespace RRExpress.Droid {

    [BroadcastReceiver]
    [IntentFilter(new string[] {
        "com.baidu.android.pushservice.action.MESSAGE",
        "com.baidu.android.pushservice.action.RECEIVE",
        "com.baidu.android.pushservice.action.notification.CLICK"
    })]
    public class BaiduPushReceiver : PushMessageReceiver {
        public override void OnBind(Context p0, int errorCode, string p2, string p3, string p4, string p5) {
            
        }

        public override void OnDelTags(Context p0, int p1, IList<string> p2, IList<string> p3, string p4) {
            
        }

        public override void OnListTags(Context p0, int p1, IList<string> p2, string p3) {
            
        }

        public override void OnMessage(Context p0, string p1, string p2) {
            
        }

        public override void OnNotificationArrived(Context p0, string p1, string p2, string p3) {
            
        }

        public override void OnNotificationClicked(Context p0, string p1, string p2, string p3) {
            
        }

        public override void OnSetTags(Context p0, int p1, IList<string> p2, IList<string> p3, string p4) {
            
        }

        public override void OnUnbind(Context p0, int p1, string p2) {
            
        }
    }
}