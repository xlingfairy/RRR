
using Android.App;
using Android.Content;
using CN.Jpush.Android.Service;
using RRExpress.Droid.JPushHandlers;

namespace RRExpress.Droid {
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new string[] {
        "cn.jpush.android.intent.REGISTRATION",
        "cn.jpush.android.intent.UNREGISTRATION" ,
        "cn.jpush.android.intent.MESSAGE_RECEIVED",
        "cn.jpush.android.intent.NOTIFICATION_RECEIVED",
        "cn.jpush.android.intent.NOTIFICATION_OPENED",
        "cn.jpush.android.intent.ACTION_RICHPUSH_CALLBACK",
        "cn.jpush.android.intent.CONNECTION"
    }, Categories = new string[] {
        "com.halogo.bt" //一定要和包名一致
    })]
    public class JPushReceiver : PushReceiver {

        public async override void OnReceive(Context ctx, Intent intent) {
            base.OnReceive(ctx, intent);

            var action = intent.Action;
            System.Diagnostics.Debug.WriteLine(action);

            var bundle = intent.Extras;
            await ReceiverHandler.Handle(intent.Action, bundle);
        }
    }
}