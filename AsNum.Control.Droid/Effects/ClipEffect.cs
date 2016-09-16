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
using AsNum.XFControls.Droid.Effects;
using Android.Graphics.Drawables;

//[assembly: ExportEffect(typeof(ClipEffect), "ClipEffect")]
namespace AsNum.XFControls.Droid.Effects {

    [Obsolete("Î´Íê³É")]
    public class ClipEffect : PlatformEffect {
        protected override void OnAttached() {
            //this.Control.ClipBounds = new Android.Graphics.Rect(0, 0, 50, 0);
            var img = (ImageView)this.Control;
            img.Drawable.SetBounds(0, 0, 10, 0);
            img.Drawable.InvalidateSelf();
        }

        protected override void OnDetached() {

        }
    }
}