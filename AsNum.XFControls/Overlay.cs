using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// ∏≤∏«≤„
    /// </summary>
    public partial class Overlay : ContentView {

        /// <summary>
        ///  «∑Òœ‘ æ’⁄’÷≤„
        /// </summary>
        public BindableProperty WithMaskProperty =
            BindableProperty.Create("WithMask",
                typeof(bool),
                typeof(Overlay),
                true);

        /// <summary>
        /// ’⁄’÷≤„—’…´
        /// </summary>
        public BindableProperty MaskColorProperty =
            BindableProperty.Create("MaskColor",
                typeof(Color),
                typeof(Overlay),
                Color.FromHex("aa999999"));

        /// <summary>
        ///  «∑Òœ‘ æ
        /// </summary>
        public BindableProperty IsShowProperty =
            BindableProperty.Create("IsShow",
                typeof(bool),
                typeof(Overlay),
                false,
                propertyChanged: IsShowChanged
            );


        public bool WithMask {
            get {
                return (bool)this.GetValue(WithMaskProperty);
            }
            set {
                this.SetValue(WithMaskProperty, value);
            }
        }

        public Color MaskColor {
            get {
                return (Color)this.GetValue(MaskColorProperty);
            }
            set {
                this.SetValue(MaskColorProperty, value);
            }
        }

        public bool IsShow {
            get {
                return (bool)this.GetValue(IsShowProperty);
            }
            set {
                this.SetValue(IsShowProperty, value);
            }
        }

        private static void IsShowChanged(BindableObject bindable, object oldValue, object newValue) {
            var flag = (bool)newValue;
            var overlay = (Overlay)bindable;
            overlay.IsVisible = flag;
        }

        public void Show() {
            this.IsShow = true;
        }

        public void Hide() {
            this.IsShow = false;
        }
    }
}