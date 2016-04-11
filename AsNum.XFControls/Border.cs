using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace AsNum.XFControls {
    [DebuggerDisplay("TopLeft={Left}, TopRight={Top}, BottomRight={Right}, BottomLeft={Bottom}")]
    [TypeConverter(typeof(CornerRadiusConverter))]
    public struct CornerRadius {
        public double TopLeft {
            get;
            set;
        }

        public double TopRight {
            get;
            set;
        }

        public double BottomRight {
            get;
            set;
        }

        public double BottomLeft {
            get;
            set;
        }

        public double HorizontalThickness {
            get {
                return this.TopLeft + this.BottomRight;
            }
        }


        public CornerRadius(double uniformSize) {
            this = new CornerRadius(uniformSize, uniformSize, uniformSize, uniformSize);
        }

        public CornerRadius(double horizontalSize, double verticalSize) {
            this = new CornerRadius(horizontalSize, verticalSize, horizontalSize, verticalSize);
        }

        public CornerRadius(double tl, double tr, double br, double bl) {
            this = default(CornerRadius);
            this.TopLeft = tl;
            this.TopRight = tr;
            this.BottomRight = br;
            this.BottomLeft = bl;
        }

        public static implicit operator CornerRadius(Size size) {
            return new CornerRadius(size.Width, size.Height, size.Width, size.Height);
        }


        public static implicit operator CornerRadius(double uniformSize) {
            return new CornerRadius(uniformSize);
        }

        private bool Equals(CornerRadius other) {
            return this.TopLeft.Equals(other.TopLeft)
                && this.TopRight.Equals(other.TopRight)
                && this.BottomRight.Equals(other.BottomRight)
                && this.BottomLeft.Equals(other.BottomLeft);
        }


        public static bool operator ==(CornerRadius left, CornerRadius right) {
            return left.Equals(right);
        }

        public static bool operator !=(CornerRadius left, CornerRadius right) {
            return !left.Equals(right);
        }

        public override int GetHashCode() {
            return ((this.TopLeft.GetHashCode() * 397
                ^ this.TopRight.GetHashCode()) * 397
                ^ this.BottomRight.GetHashCode()) * 397
                ^ this.BottomLeft.GetHashCode();
        }

        public override bool Equals(object obj) {
            return obj != null && obj is CornerRadius && this.Equals((CornerRadius)obj);
        }

    }

    public class CornerRadiusConverter : TypeConverter {
        public override bool CanConvertFrom(Type sourceType) {
            if (sourceType == null) {
                throw new ArgumentNullException("sourceType");
            }
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(CultureInfo culture, object value) {
            if (value == null) {
                return null;
            }
            string text = value as string;
            if (text != null) {
                string[] array = text.Split(new char[]
                {
                    ','
                });
                switch (array.Length) {
                    case 1: {
                            double num;
                            if (double.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out num)) {
                                return new CornerRadius(num);
                            }
                            break;
                        }
                    case 2: {
                            double num;
                            double num2;
                            if (double.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out num) && double.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out num2)) {
                                return new CornerRadius(num, num2);
                            }
                            break;
                        }
                    case 4: {
                            double num;
                            double num2;
                            double right;
                            double bottom;
                            if (double.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out num) && double.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out num2) && double.TryParse(array[2], NumberStyles.Number, CultureInfo.InvariantCulture, out right) && double.TryParse(array[3], NumberStyles.Number, CultureInfo.InvariantCulture, out bottom)) {
                                return new CornerRadius(num, num2, right, bottom);
                            }
                            break;
                        }
                }
            }
            throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", new object[]
            {
                value,
                typeof(CornerRadius)
            }));
        }

    }
    public class Border : ContentView {

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create<Border, CornerRadius>(p => p.CornerRadius, default(CornerRadius));
        public static readonly BindableProperty StrokeProperty = BindableProperty.Create<Border, Color>(p => p.Stroke, Color.Transparent);
        public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create<Border, Thickness>(p => p.StrokeThickness, default(Thickness));
        public static readonly BindableProperty IsClippedToBorderProperty = BindableProperty.Create<Border, bool>(p => p.IsClippedToBorder, true);

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
