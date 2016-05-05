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

            if (this.Control != null)
                this.Control.ValueChanged -= Control_ValueChanged;


            var picker = new NumberPicker(this.Context);
            //var picker = new ColorNumberPicker(this.Context, this.Element.TextColor.ToAndroid(), this.Element.DividerColor.ToAndroid());
            picker.WrapSelectorWheel = false;
            picker.DescendantFocusability = DescendantFocusability.BlockDescendants;


            this.SetNativeControl(picker);
            this.UpdatePicker();
            this.Control.ValueChanged += Control_ValueChanged;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(DataPicker.ItemsSourceProperty.PropertyName)) {
                this.UpdatePicker();
            }
        }


        private void UpdatePicker() {
            if (this.Element.ItemsSource != null) {

                var c = ((IEnumerable<object>)this.Element.ItemsSource).Count() - 1;
                var cc = this.Control.MaxValue;

                if (c <= cc) {
                    this.Control.MaxValue = c;
                    this.Control.SetDisplayedValues(this.Element.StringValues.ToArray());
                } else {
                    this.Control.SetDisplayedValues(this.Element.StringValues.ToArray());
                    this.Control.MaxValue = c;
                }

                this.Control.MinValue = 0;

                this.Control.Value = this.Element.SelectedIndex;

                //this.UpdatePickerColor();
            }
        }


        private void Control_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e) {
            this.Element.SelectedItem = ((IEnumerable<object>)this.Element.ItemsSource).ElementAt(e.NewVal);
        }

        //private void UpdatePickerColor() {
        //    for (var i = 0; i < this.Control.ChildCount; i++) {
        //        var c = this.Control.GetChildAt(i);
        //        if (c is EditText) {
        //            var edt = (EditText)c;
        //            edt.SetTextColor(this.Element.TextColor.ToAndroid());
        //        }
        //    }
        //}
    }
}