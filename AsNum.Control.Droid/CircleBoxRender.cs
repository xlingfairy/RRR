using Android.Graphics;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CircleBox), typeof(CircleBoxRender))]
namespace AsNum.XFControls.Droid {
    public class CircleBoxRender : VisualElementRenderer<CircleBox> {

        protected override void OnElementChanged(ElementChangedEventArgs<CircleBox> e) {
            base.OnElementChanged(e);
            this.Element.HorizontalOptions = LayoutOptions.Center;
            this.Element.VerticalOptions = LayoutOptions.Center;

            if (this.Element.Content != null) {
                this.Element.Content.HorizontalOptions = LayoutOptions.Center;
                this.Element.Content.VerticalOptions = LayoutOptions.Center;
            }
        }

        public override void Draw(Canvas canvas) {
            var density = this.Context.Resources.DisplayMetrics.Density;
            var path = new Path();
            path.AddCircle(canvas.Width / 2, canvas.Height / 2, (float)this.Element.Radius * density, Path.Direction.Ccw);
            canvas.ClipPath(path, Region.Op.Intersect);
            canvas.DrawColor(this.Element.BackgroundColor.ToAndroid());

            base.Draw(canvas);
            path.Dispose();
        }
    }
}