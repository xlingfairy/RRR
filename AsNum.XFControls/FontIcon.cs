using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AsNum.XFControls
{
    public class FontIcon : View {

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(FontIcon), "");

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(double), typeof(FontIcon), 12d);

        public static readonly BindableProperty GlyphProperty =
            BindableProperty.Create("Glyph", typeof(string), typeof(FontIcon), "");

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color", typeof(Color), typeof(FontIcon), Color.Black);


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

        public Color Color {
            get {
                return (Color)this.GetValue(ColorProperty);
            }
            set {
                this.SetValue(ColorProperty, value);
            }
        }
    }
}
