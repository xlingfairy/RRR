using NControl.Abstractions;
using NGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class CheckBox : NControlView {

        #region IsChecked
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create("IsChecked",
                typeof(bool),
                typeof(CheckBox),
                false,
                BindingMode.TwoWay,
                propertyChanged: IsCheckedChanged
                );


        public bool IsChecked {
            get {
                return (bool)this.GetValue(IsCheckedProperty);
            }
            set {
                this.SetValue(IsCheckedProperty, value);
            }
        }

        private static void IsCheckedChanged(BindableObject bindable, object oldValue, object newValue) {
            throw new NotImplementedException();
        }
        #endregion

        #region IsShowLabel
        public static readonly BindableProperty IsShowLabelProperty =
            BindableProperty.Create("ShowLabel",
                typeof(bool),
                typeof(CheckBox),
                false
                );


        public bool IsShowLabel {
            get {
                return (bool)this.GetValue(IsShowLabelProperty);
            }
            set {
                this.SetValue(IsShowLabelProperty, value);
            }
        }

        #endregion

        #region Label
        public static readonly BindableProperty LabelProperty =
            BindableProperty.Create("Label",
                typeof(string),
                typeof(CheckBox),
                null,
                propertyChanged: LabelChanged);


        public string Label {
            get {
                return (string)this.GetValue(LabelProperty);
            }
            set {
                this.SetValue(LabelProperty, value);
            }
        }

        private static void LabelChanged(BindableObject bindable, object oldValue, object newValue) {
            var asm = typeof(CheckBox).GetTypeInfo().Assembly;
            var stmReader = new StreamReader(asm.GetManifestResourceStream(""));
            var svgReader = new SvgReader(stmReader);
        }
        #endregion
    }
}
