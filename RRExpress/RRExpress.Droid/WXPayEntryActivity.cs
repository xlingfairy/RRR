using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Com.Tencent.MM.Sdk.Constants;
using Com.Tencent.MM.Sdk.Modelbase;
using Com.Tencent.MM.Sdk.Openapi;
using RRExpress.Droid.WXPay;

namespace RRExpress.Droid {
    [Activity(Name = "rrExpress.Droid.wxapi.WXPayEntryActivity",
        Exported = true,
        Label = "WXPayEntryActivity")]
    public class WXPayEntryActivity : Activity, IWXAPIEventHandler {

        private IWXAPI msgApi;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            msgApi = WXAPIFactory.CreateWXAPI(this.ApplicationContext, Constants.APPID);
        }

        protected override void OnNewIntent(Intent intent) {
            base.OnNewIntent(intent);
            this.Intent = (intent);
            msgApi.HandleIntent(intent, this);
        }

        /// <summary>
        ///  接受微信发来的消息
        /// </summary>
        /// <param name="p0"></param>
        public void OnReq(BaseReq p0) {
            switch (p0.Type) {
                default:
                    break;
            }
        }

        /// <summary>
        /// 接受发往微信的消息的回调
        /// </summary>
        /// <param name="p0"></param>
        public void OnResp(BaseResp p0) {
            //int result = 0;

            Toast.MakeText(this, "支付结果:" + p0.ErrStr, ToastLength.Long).Show();

            if (p0.Type == ConstantsAPI.CommandPayByWx) {
                switch (p0.ErrorCode) {
                    case BaseResp.ErrCode.ErrOk:
                        break;
                    case BaseResp.ErrCode.ErrSentFailed:
                        break;
                    case BaseResp.ErrCode.ErrAuthDenied:
                        break;
                    case BaseResp.ErrCode.ErrUserCancel:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}