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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Reflection;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using Android.Util;

[assembly: ExportRenderer(typeof(PickerEx), typeof(PickerExRender))]
namespace AsNum.XFControls.Droid {

    public class PickerExRender : PickerRenderer {

        private AlertDialog Dialog;
        private FieldInfo DialogField;

        public PickerExRender() {
            this.DialogField = typeof(PickerRenderer).GetField("dialog", BindingFlags.NonPublic | BindingFlags.Instance);
            //if (f != null) {
            //    //base dialog在 OnClick 的时候才赋值
            //    this.dlg = (AlertDialog)f.GetValue(this);
            //}
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null && this.DialogField != null) {
                this.Control.Tag = this;
                this.Control.SetOnClickListener(new PickerListener());

                var ele = (PickerEx)this.Element;
                this.Control.SetTextColor(ele.TextColor.ToAndroid());

                this.Control.Gravity =
                    ele.HorizontalTextAlignment.ToHorizontalGravityFlags();

                this.Control.SetTextSize(ComplexUnitType.Sp, (float)ele.FontSize);
            }
        }

        private void OnClick() {
            var picker = new NumberPicker(this.Context);
            if (this.Element.Items != null && this.Element.Items.Count > 0) {
                picker.MaxValue = this.Element.Items.Count - 1;
                picker.MinValue = 0;
                picker.SetDisplayedValues(this.Element.Items.ToArray());
                picker.WrapSelectorWheel = false;
                picker.DescendantFocusability = DescendantFocusability.BlockDescendants;
                picker.Value = this.Element.SelectedIndex;
            }

            var linearLayout = new LinearLayout(this.Context) {
                Orientation = Orientation.Vertical
            };

            linearLayout.AddView((Android.Views.View)picker);

            //this.Element.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);
            this.Element.Focus();

            AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);
            builder.SetView(linearLayout);
            builder.SetTitle(this.Element.Title ?? "");
            builder.SetNegativeButton(17039360, (EventHandler<DialogClickEventArgs>)((s, a) => {

                //this.Element.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                this.Element.Unfocus();

                this.Control.ClearFocus();
                Dialog = null;
            }));
            builder.SetPositiveButton(17039370, (EventHandler<DialogClickEventArgs>)((s, a) => {
                //this.Element.SetValueFromRenderer(Picker.SelectedIndexProperty, (object)picker.Value);
                this.Element.SelectedIndex = picker.Value;

                if (this.Element.Items.Count > 0 && this.Element.SelectedIndex >= 0)
                    this.Control.Text = this.Element.Items[this.Element.SelectedIndex];

                //this.Element.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, (object)false);
                this.Element.Unfocus();

                this.Control.ClearFocus();
                Dialog = null;
            }));
            Dialog = builder.Create();
            //this.DialogField.SetValue(this, this.Dialog);
            Dialog.Show();
        }

        private class PickerListener : Java.Lang.Object, Android.Views.View.IOnClickListener, IJavaObject, IDisposable {
            public static readonly PickerListener Instance = new PickerListener();

            public void OnClick(Android.Views.View v) {
                var render = v.Tag as PickerExRender;
                if (render == null)
                    return;
                render.OnClick();
            }
        }
    }

}