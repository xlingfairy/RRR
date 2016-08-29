using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Android.Animation;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views.Animations;
using Android.Util;


//https://github.com/siriscac/RippleView/blob/master/RippleView/src/com/indris/material/RippleView.java
[assembly: ExportEffect(typeof(TapEffect), "TapEffect")]
namespace AsNum.XFControls.Droid.Effects {
    public class TapEffect : PlatformEffect {
        private RippleDrawable Drb;

        protected override void OnAttached() {

            this.Drb = new RippleDrawable();
            this.Container.Background = this.Drb;

            this.Container.Touch += Container_Touch;
        }

        private void Container_Touch(object sender, Android.Views.View.TouchEventArgs e) {
            this.Drb.X = e.Event.GetX();
            this.Drb.Y = e.Event.GetY();
            this.Drb.Bounds = new Rect(this.Container.Left, this.Container.Top, this.Container.Width, this.Container.Width);

            switch (e.Event.Action) {
                case MotionEventActions.Down:
                    break;
                case MotionEventActions.Move:
                    break;
                case MotionEventActions.Up:
                    var radiusAni = ObjectAnimator.OfInt(10, 500);
                    radiusAni.Update += (s, arg) => this.Drb.Radius = (int)arg.Animation.AnimatedValue;
                    radiusAni.SetInterpolator(new AccelerateDecelerateInterpolator());
                    radiusAni.SetDuration(500);

                    var alphaAni = ObjectAnimator.OfInt(500, 1);
                    alphaAni.Update += (s, arg) => this.Drb.Radius = ((int)arg.Animation.AnimatedValue);
                    alphaAni.SetDuration(500);
                    radiusAni.SetInterpolator(new AccelerateDecelerateInterpolator());

                    var set = new AnimatorSet();
                    set.Play(radiusAni).Before(alphaAni);
                    set.Start();

                    break;
            }

            e.Handled = false;
        }

        protected override void OnDetached() {
            this.Container.Touch -= Container_Touch;
        }


        class RippleDrawable : Drawable {
            public float X { get; set; }

            public float Y { get; set; }

            private Path path = new Path();

            private Paint Paint;

            private int _radius = 0;
            public int Radius {
                get {
                    return this._radius;
                }
                set {
                    this._radius = value;
                    var rg = new RadialGradient(this.X,
                        this.Y,
                        value,
                        Android.Graphics.Color.Gray,
                        Android.Graphics.Color.White,
                        Shader.TileMode.Mirror
                        );

                    this.Paint.SetShader(rg);
                    this.InvalidateSelf();
                }
            }



            public override int Opacity {
                get {
                    return 0;
                }
            }

            public RippleDrawable() {
                this.Paint = new Paint(PaintFlags.AntiAlias);
                this.Paint.Alpha = 100;
            }

            public override void Draw(Canvas canvas) {
                canvas.Save(SaveFlags.Clip);
                this.path.Reset();
                this.path.AddCircle(this.X, this.Y, this._radius, Path.Direction.Cw);
                canvas.ClipPath(this.path);
                canvas.Restore();

                canvas.DrawCircle(this.X, this.Y, this._radius, this.Paint);
            }

            public override void SetAlpha(int alpha) {
                this.Paint.Alpha = alpha;
            }

            public override void SetColorFilter(ColorFilter colorFilter) {
            }
        }
    }
}