using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls.Binders {
    public class LabelBinder {

        public static BindableProperty DefaultTextProperty =
            BindableProperty.CreateAttached(
                "DefaultText",
                typeof(string),
                typeof(LabelBinder),
                "-",
                propertyChanged: Changed);

        public static void SetDefaultText(BindableObject bindable, object value) {
            bindable.SetValue(DefaultTextProperty, value);
        }

        public static object GetDefaultText(BindableObject bindable) {
            return bindable.GetValue(DefaultTextProperty);
        }

        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            if (!(bindable is Label))
                throw new NotSupportedException("LabelBinder 只支持 Label");

            if (oldValue != null) {

            }

            var lbl = (Label)bindable;
            SetDefaultText(lbl);
            lbl.PropertyChanged += Lbl_PropertyChanged;
        }

        private static void Lbl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals(Label.TextProperty.PropertyName)) {
                var lbl = (Label)sender;
                SetDefaultText(lbl);
            }
        }

        private static void SetDefaultText(Label lbl) {
            if (string.IsNullOrEmpty(lbl.Text)) {
                lbl.Text = (string)GetDefaultText(lbl);
            }
        }
    }
}
