using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TimePickerEx), typeof(TimePickerExRender))]
namespace AsNum.XFControls.Droid {
    public class TimePickerExRender : TimePickerRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e) {
            base.OnElementChanged(e);

            this.UpdateTextColor();
            this.UpdatePlaceHolder();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(DatePickerEx.TextColorProperty.PropertyName)) {
                this.UpdateTextColor();
            }
        }


        private void UpdateTextColor() {
            var ele = (TimePickerEx)this.Element;
            this.Control.SetTextColor(ele.TextColor.ToAndroid());
        }

        private void UpdatePlaceHolder() {
            var ele = (TimePickerEx)this.Element;
            this.Control.Hint = ele.PlaceHolder ?? "";
            this.Control.SetHintTextColor(ele.PlaceHolderColor.ToAndroid());
        }

    }
}