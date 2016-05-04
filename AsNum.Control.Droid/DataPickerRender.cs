using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using Xamarin.Forms;
using System.Reflection;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(DataPicker), typeof(DataPickerRender))]
namespace AsNum.XFControls.Droid {
    public class DataPickerRender : ViewRenderer<DataPicker, NumberPicker> {

        protected override void OnElementChanged(ElementChangedEventArgs<DataPicker> e) {
            base.OnElementChanged(e);

            var picker = new NumberPicker(this.Context);
            this.SetNativeControl(picker);
            this.UpdatePicker();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(DataPicker.ItemsSourceProperty)) {
                this.UpdatePicker();
            }
        }


        private void UpdatePicker() {
            this.Control.MaxValue = ((IEnumerable<object>)this.Element.ItemsSource).Count();
            this.Control.MinValue = 0;

            this.Control.SetDisplayedValues(this.Element.StringValues.ToArray());
            this.Control.Value = this.Element.SelectedIndex;
            this.Control.ValueChanged += Control_ValueChanged;
        }

        private void Control_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e) {

        }
    }
}