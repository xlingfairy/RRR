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
using Xamarin.Forms.Platform.Android;
using AW = Android.Webkit;
using Android.Webkit;
using Android.Graphics;
using Xamarin.Forms;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using Java.Interop;
using System.ComponentModel;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(WebBaiduMap), typeof(WebBaiduMapRender))]
namespace AsNum.XFControls.Droid {
    public class WebBaiduMapRender : ViewRenderer<WebBaiduMap, AW.WebView> {

        protected override void OnElementChanged(ElementChangedEventArgs<WebBaiduMap> e) {
            base.OnElementChanged(e);

            if (e.OldElement != null) {
                if (this.Control != null)
                    this.Control.RemoveJavascriptInterface("CS");
            }

            if (e.NewElement != null) {
                //https://developer.xamarin.com/guides/cross-platform/advanced/razor_html_templates/
                var ctrl = new AW.WebView(this.Context);
                ctrl.Settings.SetGeolocationEnabled(true);
                ctrl.Settings.JavaScriptEnabled = true;

                var client = new BaiduMapWebViewClient();
                client.OnPageLoaded += Client_OnPageLoaded;
                ctrl.SetWebViewClient(client);
                ctrl.SetWebChromeClient(new BaiduMapWebChromeClient());

                // https://developer.xamarin.com/recipes/android/controls/webview/call_csharp_from_javascript/
                ctrl.AddJavascriptInterface(new BaiduMapJsInterface(this.Context), "CS");
                this.SetNativeControl(ctrl);
                this.Control.LoadUrl("file:///android_asset/BaiduMap.html");

                //this.Init();
            }
        }

        private void Client_OnPageLoaded(object sender, EventArgs e) {
            this.Init();
        }

        private void Init() {
            if (!string.IsNullOrEmpty(this.Element.AK)) {
                this.ExecuteJs(string.Format("page.Init('{0}')", this.Element.AK));
            }
        }

        private void ExecuteJs(string script) {
            this.Control.LoadUrl(string.Format("javascript:{0};", script));
        }








        public class BaiduMapWebViewClient : WebViewClient {

            public event EventHandler OnPageLoaded = null;

            public override bool ShouldOverrideUrlLoading(AW.WebView view, string url) {
                return base.ShouldOverrideUrlLoading(view, url);
            }

            public override void OnPageFinished(AW.WebView view, string url) {
                base.OnPageFinished(view, url);
                if (this.OnPageLoaded != null)
                    this.OnPageLoaded.Invoke(view, new EventArgs());
            }

        }











        public class BaiduMapWebChromeClient : WebChromeClient {
            public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback) {
                //base.OnGeolocationPermissionsShowPrompt(origin, callback);
                callback.Invoke(origin, true, false);
            }
        }






        public class BaiduMapJsInterface : Java.Lang.Object {

            private Context Context { get; }

            public BaiduMapJsInterface(Context context) {
                this.Context = context;
            }

            [Export]
            [JavascriptInterface]
            public void ShowToast(string msg) {
                Toast.MakeText(this.Context, msg, ToastLength.Long)
                    .Show();
            }
        }
    }
}