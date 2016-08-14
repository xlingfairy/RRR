using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class RadioButtonGroup : RadioGroupBase {

        #region ShowRadio
        public static BindableProperty ShowRadioProperty =
            BindableProperty.Create("ShowRadio",
                            typeof(bool),
                            typeof(RadioButtonGroup),
                            false
                );

        public bool ShowRadio {
            get {
                return (bool)this.GetValue(ShowRadioProperty);
            }
            set {
                this.SetValue(ShowRadioProperty, value);
            }
        }
        #endregion



        protected override Layout<View> GetContainer() {
            return new WrapLayout();
        }

        protected override Radio GetRadio(object data) {
            var radio = base.GetRadio(data);
            radio.SetBinding(Radio.ShowRadioProperty, new Binding("ShowRadio", source: this));
            return radio;
        }
    }
}
