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
using AsNum.XFControls.Droid;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryRender))]
namespace AsNum.XFControls.Droid {
    public class EntryRender : EntryRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);

            if (this.Element != null) {
                //this.UpdateAlign();
                //this.Control.SetIncludeFontPadding(false);
                this.Control.SetPadding(0, 20, 0, 20);
            }
        }

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
        //    base.OnElementPropertyChanged(sender, e);

        //    if (e.PropertyName.Equals(Entry.HorizontalTextAlignmentProperty.PropertyName)) {
        //        this.UpdateAlign();
        //    }
        //}

        //private void UpdateAlign() {
        //    this.Control.Gravity =
        //        Helper.ToHorizontalGravityFlags(this.Element.HorizontalTextAlignment) |
        //         GravityFlags.Bottom;
        //}
    }
}