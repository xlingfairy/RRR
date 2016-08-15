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
using Xamarin.Forms;
using AsNum.XFControls;
using AsNum.XFControls.Droid;

[assembly: ExportRenderer(typeof(Popup), typeof(PopupRender))]
namespace AsNum.XFControls.Droid {
    public class PopupRender : VisualElementRenderer<Popup> {

        private FrameLayout DecoreView {
            get { return (FrameLayout)((Activity)Forms.Context).Window.DecorView; }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Popup> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var render = Platform.CreateRenderer(e.NewElement.View);
                Platform.SetRenderer(e.NewElement, render);
                this.DecoreView.AddView(render.ViewGroup, 200, 500/*, new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)*/);
            }
        }

    }
}