using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class RadioGroup : RadioGroupBase {

        #region Orientation
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation",
                                    typeof(StackOrientation),
                                    typeof(RadioGroup),
                                    StackOrientation.Horizontal,
                                    propertyChanged: OrientationChanged);

        private static void OrientationChanged(BindableObject bindable, object oldValue, object newValue) {
            var rg = (RadioGroup)bindable;
            ((StackLayout)rg.Container).Orientation = (StackOrientation)newValue;
            
        }

        public StackOrientation Orientation {
            get {
                return (StackOrientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }
        #endregion

        protected override Layout<View> GetContainer() {
            return new StackLayout() {
                Orientation = this.Orientation
            };
        }
    }
}
