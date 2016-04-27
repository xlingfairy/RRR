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

[assembly: ExportRenderer(typeof(WebBaiduMap), typeof(WebBaiduMapRender))]
namespace AsNum.XFControls.Droid {
    public class WebBaiduMapRender : ViewRenderer<WebBaiduMap, AW.WebView> {

        protected override void OnElementChanged(ElementChangedEventArgs<WebBaiduMap> e) {
            base.OnElementChanged(e);

            if (e.OldElement != null) {
                if (this.Control != null)
                    this.Control.RemoveJavascriptInterface("native");
            }

            if (e.NewElement != null) {
                //https://developer.xamarin.com/guides/cross-platform/advanced/razor_html_templates/
                var ctrl = new AW.WebView(this.Context);
                ctrl.Settings.SetGeolocationEnabled(true);
                ctrl.Settings.JavaScriptEnabled = true;
                ctrl.SetWebViewClient(new BaiduMapWebViewClient());
                ctrl.SetWebChromeClient(new BaiduMapWebChromeClient());

                // https://developer.xamarin.com/recipes/android/controls/webview/call_csharp_from_javascript/
                ctrl.AddJavascriptInterface(new BaiduMapJsInterface(this.Context), "native");
                this.SetNativeControl(ctrl);
                this.Control.LoadUrl("file:///android_asset/BaiduMap.html");
            }
        }


        public class BaiduMapWebViewClient : WebViewClient {
            public override bool ShouldOverrideUrlLoading(AW.WebView view, string url) {
                return base.ShouldOverrideUrlLoading(view, url);
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

        }
    }
}