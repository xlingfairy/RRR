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
using AW = Android.Widget;
using Xamarin.Forms;
using AsNum.XFControls;
using AsNum.XFControls.Droid;

[assembly: ExportRenderer(typeof(Overlay), typeof(OverlayRender))]
namespace AsNum.XFControls.Droid {
    public class OverlayRender : ViewRenderer<Overlay, AW.RelativeLayout> {

        protected override void OnElementChanged(ElementChangedEventArgs<Overlay> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var subView = Platform.GetRenderer(this.Element.Content);
                var pop = new PopupWindow(this.Context);
                pop.ShowAsDropDown(subView.ViewGroup, 0, 0);
            }
        }

    }
}