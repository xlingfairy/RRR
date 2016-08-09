using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AsNum.XFControls {

    /// <summary>
    /// 
    /// </summary>
    public class PickerEx : Picker {

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable<object>),
                typeof(PickerEx),
                Enumerable.Empty<object>(),
                propertyChanged: ItemsSourceChanged
                );

        public static readonly BindableProperty DefaultIndexProperty =
            BindableProperty.Create("DefaultIndex",
                typeof(int),
                typeof(PickerEx),
                0
                );

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(PickerEx),
                null,
                propertyChanged: SelectedItemChanged);

        //public static readonly BindableProperty TextColorProperty =
        //    BindableProperty.Create(
        //            "TextColor",
        //            typeof(Color),
        //            typeof(TimePickerEx),
        //            Color.Default
        //            );

        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(
                "HorizontalTextAlignment",
                typeof(TextAlignment),
                typeof(TimePickerEx),
                TextAlignment.Start,
                BindingMode.OneWay
                );

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(TimePickerEx),
                15D);

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

        //public Color TextColor {
        //    get {
        //        return (Color)this.GetValue(TextColorProperty);
        //    }
        //    set {
        //        this.SetValue(TextColorProperty, value);
        //    }
        //}


        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable<object> ItemsSource {
            get {
                return (IEnumerable<object>)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public object SelectedItem {
            get {
                return this.GetValue(SelectedItemProperty);
            }
            set {
                this.SetValue(SelectedItemProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayMember {
            get; set;
        }


        public int DefaultIndex {
            get {
                return (int)this.GetValue(DefaultIndexProperty);
            }
            set {
                this.SetValue(DefaultIndexProperty, value);
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var picker = (PickerEx)bindable;
            picker.Items.Clear();
            var datas = (IEnumerable<object>)newValue;
            if (datas == null) {
            }
            else {
                foreach (var o in datas) {
                    var d = Helper.TryGetProperty(o, picker.DisplayMember);
                    if (d != null) {
                        picker.Items.Add(d.ToString());
                    }
                }
            }
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {
            var picker = (PickerEx)bindable;
            if (newValue != null) {
                var v = Helper.TryGetProperty(newValue, picker.DisplayMember);
                if (v != null) {
                    var idx = picker.Items.IndexOf(v.ToString());
                    picker.SelectedIndex = idx;
                }
            }
        }
    }
}
