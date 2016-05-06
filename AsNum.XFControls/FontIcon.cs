using AsNum.XFControls.Binders;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class FontIcon : View {

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(FontIcon), "");

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(double), typeof(FontIcon), 12d);

        public static readonly BindableProperty GlyphProperty =
            BindableProperty.Create("Glyph", typeof(string), typeof(FontIcon), "");


        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(
                "Color",
                typeof(Color),
                typeof(FontIcon),
                Color.Black,
                propertyChanged: Changed);


        public static readonly BindableProperty DisableColorProperty =
            BindableProperty.Create("DisableColor", typeof(Color), typeof(FontIcon), Color.Gray);


        public static readonly BindableProperty TapCmdProperty =
            BindableProperty.Create("TapCmd",
                typeof(ICommand),
                typeof(FontIcon),
                null,
                propertyChanged: TapCmdChanged
                );

        public static readonly BindableProperty TapCmdParamProperty =
            BindableProperty.Create("TapParam",
                typeof(object),
                typeof(FontIcon),
                null,
                propertyChanged: TapCmdParamChanged);



        public string FontFamily {
            get {
                return this.GetValue(FontFamilyProperty) as string;
            }
            set {
                this.SetValue(FontFamilyProperty, value);
            }
        }

        public double FontSize {
            get {
                return (double)this.GetValue(FontSizeProperty);
            }
            set {
                this.SetValue(FontSizeProperty, value);
            }
        }

        public string Glyph {
            get {
                return this.GetValue(GlyphProperty) as string;
            }
            set {
                this.SetValue(GlyphProperty, value);
            }
        }

        /// <summary>
        /// 当前使用颜色，在 Renderer 中使用
        /// </summary>
        public Color CurrentColor {
            get;
            private set;
        }

        public Color Color {
            get {
                return (Color)this.GetValue(ColorProperty);
            }
            set {
                this.SetValue(ColorProperty, value);
            }
        }

        public Color DisableColor {
            get {
                return (Color)this.GetValue(DisableColorProperty);
            }
            set {
                this.SetValue(DisableColorProperty, value);
            }
        }


        public ICommand TapCmd {
            get {
                return (ICommand)this.GetValue(TapCmdProperty);
            }
            set {
                this.SetValue(TapCmdProperty, value);
            }
        }

        public object TapParam {
            get {
                return this.GetValue(TapCmdParamProperty);
            }
            set {
                this.SetValue(TapCmdParamProperty, value);
            }
        }

        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var fi = (FontIcon)bindable;
            fi.UpdateColor();
        }

        private void UpdateColor() {
            this.CurrentColor = this.IsEnabled ? this.Color : this.DisableColor;
            this.OnPropertyChanged("CurrentColor");
        }

        private static void TapCmdParamChanged(BindableObject bindable, object oldValue, object newValue) {
            TapBinder.SetParam(bindable, newValue);
        }

        private static void TapCmdChanged(BindableObject bindable, object oldValue, object newValue) {
            TapBinder.SetCmd(bindable, (ICommand)newValue);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals("IsEnabled")) {
                this.UpdateColor();
            }
        }
    }
}
