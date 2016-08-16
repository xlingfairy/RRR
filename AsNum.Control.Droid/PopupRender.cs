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
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Popup), typeof(PopupRender))]
namespace AsNum.XFControls.Droid {
    public class PopupRender : VisualElementRenderer<Popup> {

        private FrameLayout DecoreView = (FrameLayout)((Activity)Forms.Context).Window.DecorView;

        public Rectangle ScreenSize {
            get {
                return new Rectangle(
                    0.0,
                    0.0,
                    ContextExtensions.FromPixels(Forms.Context, DecoreView.Width),
                    ContextExtensions.FromPixels(Forms.Context, DecoreView.Height)
                    );
            }
        }

        private IVisualElementRenderer Render = null;

        public PopupRender() {
            FormsAppCompatActivity.BackPressed += FormsAppCompatActivity_BackPressed;
            FormsApplicationActivity.BackPressed += FormsAppCompatActivity_BackPressed;
        }

        private bool FormsAppCompatActivity_BackPressed(object sender, EventArgs e) {
            if (this.Render != null)
                this.DecoreView.RemoveView(this.Render.ViewGroup);

            return true;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Popup> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var size = this.ScreenSize;
                this.Render = this.Element.View.GetOrCreateRenderer();

                //var size2 = render.Element.Measure(size.Width, size.Height);
                //render.ViewGroup.Layout((int)size.Left, (int)size.Top, (int)size.Right, (int)size.Bottom);
                //render.Element.Layout(new Rectangle(0, 0, size2.Request.Width, size2.Request.Height));
                this.DecoreView.AddView(this.Render.ViewGroup, LayoutParams.WrapContent, LayoutParams.WrapContent);

                this.UpdatePopupLayout();
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(Popup.WidthRequestProperty.PropertyName) ||
                e.PropertyName.Equals(Popup.HeightRequestProperty.PropertyName) ||
                e.PropertyName.Equals(Popup.XProperty.PropertyName) ||
                e.PropertyName.Equals(Popup.YProperty.PropertyName)) {

                this.UpdatePopupLayout();
            }
        }


        private void UpdatePopupLayout() {
            this.Render.Element.Layout(new Rectangle(0, 0, this.Element.WidthRequest, this.Element.HeightRequest));
            //this.Render.ViewGroup.Layout((int)this.Element.AnchorX, (int)this.Element.AnchorY, (int)(this.Element.AnchorX + this.Element.WidthRequest), (int)(this.Element.AnchorY + this.Element.HeightRequest));
            //this.Render.UpdateLayout();
            //var p = this.Render.ViewGroup.LayoutParameters;
            this.DecoreView.UpdateViewLayout(this.Render.ViewGroup, new FrameLayout.LayoutParams((int)this.Element.WidthRequest, (int)this.Element.HeightRequest) {
                TopMargin = (int)this.Element.AnchorY,
                LeftMargin = (int)this.Element.AnchorY
            });
        }
    }
}