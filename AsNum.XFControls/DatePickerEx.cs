﻿using Xamarin.Forms;

namespace AsNum.XFControls {
    public class DatePickerEx : DatePicker {

        //public static readonly BindableProperty TextColorProperty =
        //    BindableProperty.Create(
        //            "TextColor",
        //            typeof(Color),
        //            typeof(DatePickerEx),
        //            Color.Default
        //            );


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


        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(TimePickerEx),
                15D);


        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(
                "HorizontalTextAlignment",
                typeof(TextAlignment),
                typeof(DatePickerEx),
                TextAlignment.Start,
                BindingMode.OneWay
                );

        //public Color TextColor {
        //    get {
        //        return (Color)this.GetValue(TextColorProperty);
        //    }
        //    set {
        //        this.SetValue(TextColorProperty, value);
        //    }
        //}

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

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize {
            get {
                return (double)this.GetValue(FontSizeProperty);
            }
            set {
                this.SetValue(FontSizeProperty, (object)value);
            }
        }

        public TextAlignment HorizontalTextAlignment {
            get {
                return (TextAlignment)this.GetValue(HorizontalTextAlignmentProperty);
            }
            set {
                this.SetValue(HorizontalTextAlignmentProperty, value);
            }
        }
    }
}
