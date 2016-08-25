using Android.Graphics;
using Android.Graphics.Drawables;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Border), typeof(BorderRender))]
namespace AsNum.XFControls.Droid {
    public class BorderRender : VisualElementRenderer<Border> {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            BorderRendererVisual.UpdateBackground(Element, this.ViewGroup);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Border> e) {
            base.OnElementChanged(e);
            BorderRendererVisual.UpdateBackground(Element, this.ViewGroup);
        }

        protected override void DispatchDraw(Canvas canvas) {
            if (Element.IsClippedToBorder) {
                canvas.Save(SaveFlags.Clip);
                BorderRendererVisual.SetClipPath(this, canvas);
                base.DispatchDraw(canvas);
                canvas.Restore();
            } else {
                base.DispatchDraw(canvas);
            }
        }
    }


    public static class BorderRendererVisual {
        public static void UpdateBackground(Border border, Android.Views.View view) {
            var strokeThickness = border.StrokeThickness;
            var context = view.Context;

            var corners = new float[] {
                    context.ToPixels(border.CornerRadius.TopLeft),
                    context.ToPixels(border.CornerRadius.TopLeft),

                    context.ToPixels(border.CornerRadius.TopRight),
                    context.ToPixels(border.CornerRadius.TopRight),

                    context.ToPixels(border.CornerRadius.BottomRight),
                    context.ToPixels(border.CornerRadius.BottomRight),

                    context.ToPixels(border.CornerRadius.BottomLeft),
                    context.ToPixels(border.CornerRadius.BottomLeft)
                };

            GradientDrawable dab = new GradientDrawable();

            var maxWidth = (int)context.ToPixels(strokeThickness.Max());

            if (strokeThickness.HorizontalThickness + strokeThickness.VerticalThickness > 0) {
                dab.SetColor(border.BackgroundColor.ToAndroid());
                dab.SetStroke(maxWidth, border.Stroke.ToAndroid());
            }

            dab.SetCornerRadii(corners);
            dab.SetColor(border.BackgroundColor.ToAndroid());
            dab.SetCornerRadii(corners);

            var left = -(int)(maxWidth - context.ToPixels(border.StrokeThickness.Left));
            var top = -(int)(maxWidth - context.ToPixels(border.StrokeThickness.Top));
            var right = -(int)(maxWidth - context.ToPixels(border.StrokeThickness.Right));
            var bottom = -(int)(maxWidth - context.ToPixels(border.StrokeThickness.Bottom));

            var insetDab = new InsetDrawable(dab, left, top, right, bottom);

            //view.Background = dab;
            view.Background = insetDab;

            view.SetPadding(
                (int)context.ToPixels(strokeThickness.Left + border.Padding.Left),
                (int)context.ToPixels(strokeThickness.Top + border.Padding.Top),
                (int)context.ToPixels(strokeThickness.Right + border.Padding.Right),
                (int)context.ToPixels(strokeThickness.Bottom + border.Padding.Bottom));
        }

        static double Max(this Thickness t) {
            return new double[] {
                t.Left,
                t.Top,
                t.Right,
                t.Bottom
            }.Max();
        }

        static double Max(this CornerRadius t) {
            return new double[] { t.TopLeft, t.TopRight, t.BottomRight, t.BottomLeft }.Max();
        }

        public static void SetClipPath(this BorderRender br, Canvas canvas) {
            var clipPath = new Path();
            var corner = br.Element.CornerRadius;
            var tl = (float)corner.TopLeft;
            var tr = (float)corner.TopRight;
            var bbr = (float)corner.BottomRight;
            var bl = (float)corner.BottomLeft;

            //Array of 8 values, 4 pairs of [X,Y] radii
            float[] radius = new float[] {
                tl, tl, tr, tr, bbr, bbr, bl, bl
            };

            int w = (int)br.Width;
            int h = (int)br.Height;

            clipPath.AddRoundRect(new RectF(
                br.ViewGroup.PaddingLeft,
                br.ViewGroup.PaddingTop,
                w - br.ViewGroup.PaddingRight,
                h - br.ViewGroup.PaddingBottom),
                radius,
                Path.Direction.Cw);

            canvas.ClipPath(clipPath);
        }
    }
}