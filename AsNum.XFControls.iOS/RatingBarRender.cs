using AsNum.XFControls.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF = AsNum.XFControls;
using System.ComponentModel;
using CoreGraphics;

[assembly: ExportRenderer(typeof(XF.RatingBar), typeof(RatingBar))]
namespace AsNum.XFControls.iOS {
    public class RatingBarRender : ViewRenderer<XF.RatingBar, RatingBar> {

        private RatingBar RB = null;

        private bool IsDisposed = false;

        protected override void OnElementChanged(ElementChangedEventArgs<XF.RatingBar> e) {
            base.OnElementChanged(e);

            if (this.RB != null) {

            }

            this.RB = new RatingBar();
            this.SetNativeControl(this.RB);
            this.Update();
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(XF.RatingBar.IsIndicatorProperty.PropertyName) ||
                e.PropertyName.Equals(XF.RatingBar.StarCountProperty.PropertyName) ||
                e.PropertyName.Equals(XF.RatingBar.RateProperty.PropertyName) ||
                e.PropertyName.Equals(XF.RatingBar.StepProperty.PropertyName) ||
                e.PropertyName.Equals(XF.RatingBar.SelectedColorProperty.PropertyName) ||
                e.PropertyName.Equals(XF.RatingBar.UnSelectedColorProperty.PropertyName)
                ) {

                this.Update();
            }
        }


        private void Update() {
            this.RB.IsIndicator = this.Element.IsIndicator;
            this.RB.StarNum = this.Element.StarCount;
            this.RB.Rate = this.Element.Rate;
            this.RB.Step = this.Element.Step;

            //CGColor c1 = null;
            //if (!this.Element.SelectedColor.Equals(Color.Default))
            //    c1 = this.Element.SelectedColor.ToCGColor();
            //CGColor c2 = null;
            //if (!this.Element.UnSelectedColor.Equals(Color.Default))
            //    c2 = this.Element.UnSelectedColor.ToCGColor();

            //this.SetColor(c1, c2);
        }


        protected override void Dispose(bool disposing) {
            if (disposing && !this.IsDisposed) {
                if (this.RB != null) {
                    this.RB.Dispose();
                    this.RB = null;
                }
            }
            base.Dispose(disposing);
        }

    }
}
