using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using AW = Android.Widget;
using Xamarin.Forms;
using System.ComponentModel;
using AsNum.XFControls.Droid;
using AsNum.XFControls;

[assembly: ExportRenderer(typeof(RatingBar), typeof(RatingBarRender))]
namespace AsNum.XFControls.Droid {
    public class RatingBarRender : ViewRenderer<RatingBar, AW.LinearLayout> {

        private bool IsDisposed = false;

        private AW.RatingBar RB = null;

        protected override void OnElementChanged(ElementChangedEventArgs<RatingBar> e) {
            base.OnElementChanged(e);

            if (this.RB != null) {
                this.RB.RatingBarChange -= this.RB_RatingBarChange;
            }

            this.RB = new AW.RatingBar(Forms.Context);
            this.RB.RatingBarChange += RB_RatingBarChange;

            var liner = new AW.LinearLayout(Forms.Context);

            this.SetNativeControl(liner);
            this.Control.AddView(this.RB);

            this.RB.RatingBarChange += Control_RatingBarChange;
            this.Update();
        }

        private void RB_RatingBarChange(object sender, AW.RatingBar.RatingBarChangeEventArgs e) {
            this.Element.Rate = e.Rating;
        }

        private void Control_RatingBarChange(object sender, AW.RatingBar.RatingBarChangeEventArgs e) {
            this.Element.Rate = e.Rating;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(RatingBar.IsIndicatorProperty.PropertyName) ||
                e.PropertyName.Equals(RatingBar.StarCountProperty.PropertyName) ||
                e.PropertyName.Equals(RatingBar.RateProperty.PropertyName) ||
                e.PropertyName.Equals(RatingBar.StepProperty.PropertyName)
                ) {

                this.Update();
            }
        }

        protected void Update() {
            this.RB.IsIndicator = this.Element.IsIndicator;
            this.RB.NumStars = this.Element.StarCount;
            this.RB.Rating = this.Element.Rate;
            //this.RB.StepSize = this.Element.Step;
            //this.RB.Max = this.Element.StarCount;
            //this.Control.StepSize = 0.5F;
            //this.Control.SecondaryProgress = this.Element.StarCount;
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