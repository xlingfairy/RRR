using Xamarin.Forms;

namespace AsNum.XFControls {
    public class Border : ContentView {

        /// <summary>
        /// 圆角
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius",
                typeof(CornerRadius),
                typeof(Border),
                default(CornerRadius)
                );


        /// <summary>
        /// 边框颜色
        /// </summary>
        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create("Stroke",
                typeof(Color),
                typeof(Border),
                Color.Default);


        /// <summary>
        /// 边框厚度
        /// </summary>
        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create("StrokeThickness",
                typeof(Thickness),
                typeof(Border),
                default(Thickness)
                );

        /// <summary>
        /// 是否裁剪
        /// </summary>
        public static readonly BindableProperty IsClippedToBorderProperty =
            BindableProperty.Create("IsClippedToBorder",
                typeof(bool),
                typeof(bool),
                true);


        public CornerRadius CornerRadius {
            get {
                return (CornerRadius)base.GetValue(CornerRadiusProperty);
            }
            set {
                base.SetValue(CornerRadiusProperty, value);
            }
        }



        public Color Stroke {
            get {
                return (Color)GetValue(StrokeProperty);
            }
            set {
                SetValue(StrokeProperty, value);
            }
        }


        public Thickness StrokeThickness {
            get {
                return (Thickness)GetValue(StrokeThicknessProperty);
            }
            set {
                SetValue(StrokeThicknessProperty, value);
            }
        }

        public bool IsClippedToBorder {
            get {
                return (bool)GetValue(IsClippedToBorderProperty);
            }
            set {
                SetValue(IsClippedToBorderProperty, value);
            }
        }

        // cross-platform way to take into account stroke thickness
        protected override void LayoutChildren(double x, double y, double width, double height) {
            x += StrokeThickness.Left;
            y += StrokeThickness.Top;

            width -= StrokeThickness.HorizontalThickness;
            height -= StrokeThickness.VerticalThickness;

            base.LayoutChildren(x, y, width, height);
        }
    }
}
