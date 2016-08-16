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
using Xamarin.Forms;

namespace AsNum.XFControls.Droid {
    internal static class PlatformHelper {

        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable) {
            var renderer = Platform.GetRenderer(bindable);
            if (renderer == null) {
                renderer = Platform.CreateRenderer(bindable);
                Platform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }

    }
}