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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AsNum.XFControls.Droid.Effects;
using A = AsNum.XFControls.Effects;
using System.ComponentModel;

[assembly: ResolutionGroupName("AsNum")]
[assembly: ExportEffect(typeof(Shadow), "ShadowEffect")]
namespace AsNum.XFControls.Droid.Effects {

    //https://developer.xamarin.com/guides/xamarin-forms/effects/passing-parameters/clr-properties/

    public class Shadow : PlatformEffect {
        protected override void OnAttached() {
            this.Update();
        }

        protected override void OnDetached() {

        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(e);

            if (e.PropertyName.Equals(A.Shadow.RadiusProperty.PropertyName)
                || e.PropertyName.Equals(A.Shadow.ColorProperty.PropertyName)
                || e.PropertyName.Equals(A.Shadow.XProperty)
                || e.PropertyName.Equals(A.Shadow.YProperty)) {

                this.Update();
            }
        }

        private void Update() {
            try {
                var control = (Android.Widget.TextView)Control;
                if (control != null) {
                    var radius = A.Shadow.GetRadius(this.Element);
                    var x = A.Shadow.GetX(this.Element);
                    var y = A.Shadow.GetY(this.Element);
                    var color = A.Shadow.GetColor(this.Element);

                    control.SetShadowLayer(radius, x, y, color.ToAndroid());
                }
                //var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
                //if (effect != null) {
                //    float radius = effect.Radius;
                //    float distanceX = effect.DistanceX;
                //    float distanceY = effect.DistanceY;
                //    Android.Graphics.Color color = effect.Color.ToAndroid();
                //    control.SetShadowLayer(radius, distanceX, distanceY, color);
                //}
            } catch {

            }
        }
    }
}