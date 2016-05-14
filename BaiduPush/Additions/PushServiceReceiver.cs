
using Android.App;
using Android.Content;

namespace Com.Baidu.Android.Pushservice {
    [BroadcastReceiver(Name = "com.baidu.android.pushservice.PushServiceReceiver", Process = ":bdservice_v1")]
    [IntentFilter(new string[] {
        Intent.ActionBootCompleted,
        "android.net.conn.CONNECTIVITY_CHANGE",
        "com.baidu.android.pushservice.action.notification.SHOW",
        "com.baidu.android.pushservice.action.media.CLICK",
        Intent.ActionMediaMounted,
        Intent.ActionUserPresent,
        Intent.ActionPowerConnected,
        Intent.ActionPowerDisconnected
    })]
    public partial class PushServiceReceiver {
    }
}