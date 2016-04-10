using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class DatePickerEx : DatePicker {

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(
                    "TextColor",
                    typeof(Color),
                    typeof(DatePickerEx),
                    Color.Default
                    );


        public static readonly BindableProperty PlaceHolderColorProperty =
            BindableProperty.Create(
                        "PlaceHolderColor",
                        typeof(Color),
                        typeof(DatePickerEx),
                        Color.Default
                        );


        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.Create(
                "PlaceHolder",
                typeof(string),
                typeof(DatePickerEx),
                null
                );

        public Color TextColor {
            get {
                return (Color)this.GetValue(TextColorProperty);
            }
            set {
                this.SetValue(TextColorProperty, value);
            }
        }

        public Color PlaceHolderColor {
            get {
                return (Color)this.GetValue(PlaceHolderColorProperty);
            }
            set {
                this.SetValue(PlaceHolderColorProperty, value);
            }
        }

        public string PlaceHolder {
            get {
                return (string)this.GetValue(PlaceHolderProperty);
            }
            set {
                this.SetValue(PlaceHolderProperty, value);
            }
        }
    }
}
