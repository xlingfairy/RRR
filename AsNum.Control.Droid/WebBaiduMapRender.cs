using Android.Content;
using Android.Webkit;
using Android.Widget;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using Java.Interop;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static AsNum.XFControls.WebBaiduMap;
using AW = Android.Webkit;

[assembly: ExportRenderer(typeof(WebBaiduMap), typeof(WebBaiduMapRender))]
namespace AsNum.XFControls.Droid {
    public class WebBaiduMapRender : ViewRenderer<WebBaiduMap, AW.WebView> {

        private BaiduMapWebViewClient ViewClient = null;
        private BaiduMapWebChromeClient ChromeClient = null;
        private BaiduMapJsInterface JsInterface = null;

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
                ctrl.Settings.DomStorageEnabled = true;

                this.ViewClient = new BaiduMapWebViewClient();
                this.ViewClient.OnPageLoaded += Client_OnPageLoaded;
                ctrl.SetWebViewClient(this.ViewClient);

                this.ChromeClient = new BaiduMapWebChromeClient();
                ctrl.SetWebChromeClient(this.ChromeClient);

                // https://developer.xamarin.com/recipes/android/controls/webview/call_csharp_from_javascript/
                this.JsInterface = new BaiduMapJsInterface(this.Context);
                this.JsInterface.OnSearchCallback += JsInterface_OnSearchCallback;
                ctrl.AddJavascriptInterface(this.JsInterface, "CS");
                this.SetNativeControl(ctrl);
                this.Control.LoadUrl("file:///android_asset/BaiduMap.html");
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(WebBaiduMap.KeywordProperty.PropertyName)) {
                this.ExecuteJs(string.Format("page.Search('{0}')", this.Element.Keyword));
            }
        }


        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (disposing && this.Control != null) {
                this.Control.StopLoading();
                this.ViewClient.Dispose();
                this.ChromeClient.Dispose();
                this.JsInterface.Dispose();
                this.Control.Dispose();
            }
        }


        private void JsInterface_OnSearchCallback(object sender, BaiduMapJsInterface.SearchCallbackEventArgs e) {
            this.Element.SearchCallback(e.Result);
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

            public event EventHandler<SearchCallbackEventArgs> OnSearchCallback;

            public BaiduMapJsInterface(Context context) {
                this.Context = context;
            }

            [Export]
            [JavascriptInterface]
            public void ShowToast(string msg) {
                Toast.MakeText(this.Context, msg, ToastLength.Long)
                    .Show();
            }

            [Export]
            [JavascriptInterface]
            public void SearchCallback(string json) {
                if (this.OnSearchCallback != null) {
                    if (!string.IsNullOrWhiteSpace(json)) {
                        var result = JsonConvert.DeserializeObject<SearchResult>(json);
                        this.OnSearchCallback.Invoke(this, new SearchCallbackEventArgs() {
                            Result = result
                        });
                    }
                }
            }

            public class SearchCallbackEventArgs : EventArgs {
                public SearchResult Result { get; set; }
            }
        }
    }
}