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

        private ValueAnimator Anim;

        protected override void OnAttached() {

            var radius = 100;// Math.Max(this.Container.Width, this.Container.Height);

            this.Drb = new RippleDrawable();
            if (this.Container.Background != null) {
                this.Container.Background = new LayerDrawable(new Drawable[] {
                        this.Container.Background,
                        this.Drb
                });
            }
            else {
                this.Container.Background = this.Drb;
            }

            this.Container.Touch += Container_Touch;

            this.Anim = ObjectAnimator.OfInt(1, radius);
            this.Anim.Update += (s, arg) => this.Drb.Radius = (int)arg.Animation.AnimatedValue;
            this.Anim.SetInterpolator(new AccelerateDecelerateInterpolator());
            this.Anim.SetDuration(300);

            //this.Container.LayoutChange += Container_LayoutChange;
        }

        private void Container_LayoutChange(object sender, Android.Views.View.LayoutChangeEventArgs e) {
            //this.Anim = ObjectAnimator.OfInt(1, Math.Max(e.Right, e.Bottom));
        }

        private void Container_Touch(object sender, Android.Views.View.TouchEventArgs e) {
            this.Drb.X = e.Event.GetX();
            this.Drb.Y = e.Event.GetY();

            switch (e.Event.Action) {
                case MotionEventActions.Down:
                    this.Anim.Cancel();
                    this.Anim.Start();
                    break;
                case MotionEventActions.Move:
                    //this.Drb.InvalidateSelf();
                    break;
                default:
                    this.Anim.Cancel();
                    this.Anim.Reverse();
                    break;
            }

            e.Handled = false;
        }

        protected override void OnDetached() {
            try {
                if (this.Container != null) {
                    this.Container.Touch -= Container_Touch;
                    //this.Container.LayoutChange -= Container_LayoutChange;
                }
            }
            catch { }
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
                        Android.Graphics.Color.White,
                        Android.Graphics.Color.Transparent,
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