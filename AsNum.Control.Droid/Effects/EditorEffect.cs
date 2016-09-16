using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AsNum.XFControls.Droid.Effects;
using AsNum.XFControls.Binders;

[assembly: ExportEffect(typeof(EditorEffect), "EditorEffect")]
namespace AsNum.XFControls.Droid.Effects {

    public class EditorEffect : PlatformEffect {

        protected override void OnAttached() {
            if (this.Element is Editor) {
                var ctrl = (EditorEditText)this.Control;
                ctrl.Hint = EditorBinder.GetPlaceHolder(this.Element);
                ctrl.SetHintTextColor(EditorBinder.GetPlaceHolderColor(this.Element).ToAndroid());
            }
        }

        protected override void OnDetached() {

        }
    }
}