using Android.OS;
using CN.Jpush.Android.Api;

namespace RRExpress.Droid.JPushHandlers {

    /// <summary>
    /// 
    /// </summary>
    public class RegisrationHandler : BaseHandler {
        public override string Action {
            get {
                return JPushInterface.ActionRegistrationId;
                //return "cn.jpush.android.intent.REGISTRATION";
            }
        }

        public override void Handle(Bundle bundle) {
            //SDK 向 JPush Server 注册所得到的注册 全局唯一的 ID ，可以通过此 ID 向对应的客户端发送消息和通知。
            var id = bundle.GetString(JPushInterface.ExtraRegistrationId);
        }
    }
}