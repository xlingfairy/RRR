using System;
using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 园形BOX
    /// </summary>
    public class CircleBox : ContentView {

        /// <summary>
        /// 半径
        /// </summary>
        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create(
                "CircleBox",
                typeof(double),
                typeof(CircleBox),
                40d
                );

        public double Radius {
            get {
                return (Double)base.GetValue(RadiusProperty);
            }
            set {
                base.SetValue(RadiusProperty, value);
            }
        }


        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint) {
            var w = this.Radius * 2;
            return new SizeRequest(new Size(w, w));
        }

        //protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint) {
        //    return base.OnSizeRequest(widthConstraint, heightConstraint);
        //    var w = this.Radius * 2;
        //    return new SizeRequest(new Size(w, w));
        //}
    }
}
