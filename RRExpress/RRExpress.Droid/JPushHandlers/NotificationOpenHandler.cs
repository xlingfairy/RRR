using Android.OS;
using CN.Jpush.Android.Api;

namespace RRExpress.Droid.JPushHandlers {
    public class NotificationOpenHandler : BaseHandler {
        public override string Action {
            get {
                return JPushInterface.ActionNotificationOpened;
                //return "cn.jpush.android.intent.NOTIFICATION_OPENED";
            }
        }

        public override void Handle(Bundle bundle) {
            //保存服务器推送下来的通知的标题。
            //对应 API 通知内容的 title 字段。
            //对应 Portal 推送通知界面上的“通知标题”字段。
            var title = bundle.GetString(JPushInterface.ExtraNotificationTitle);

            //保存服务器推送下来的通知内容。
            //对应 API 通知内容的 alert 字段。
            //对应 Portal 推送通知界面上的“通知内容”字段。
            var ctx = bundle.GetString(JPushInterface.ExtraAlert);

            //SDK 1.2.9 以上版本支持。
            //保存服务器推送下来的附加字段。这是个 JSON 字符串。
            //对应 API 通知内容的 extras 字段。
            //对应 Portal 推送消息界面上的“可选设置”里的附加字段。
            var extra = bundle.GetString(JPushInterface.ExtraExtra);

            //SDK 1.3.5 以上版本支持。
            //通知栏的Notification ID，可以用于清除Notification
            var id = bundle.GetString(JPushInterface.ExtraNotificationId);

            //SDK 1.6.1 以上版本支持。
            //唯一标识通知消息的 ID, 可用于上报统计等。
            var msgID = bundle.GetString(JPushInterface.ExtraMsgId);
        }
    }
}