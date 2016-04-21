using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class Translate {

        public static readonly BindableProperty IsCurrentProperty =
            BindableProperty.CreateAttached("IsCurrent",
                typeof(bool),
                typeof(Translate),
                false,
                BindingMode.TwoWay,
                propertyChanged: IsCurrentChanged);


        public static bool GetIsCurrent(VisualElement bindable) {
            return (bool)bindable.GetValue(IsCurrentProperty);
        }

        public static void SetIsCurrent(VisualElement bindable, bool value) {
            bindable.SetValue(IsCurrentProperty, value);
        }


        private static void IsCurrentChanged(BindableObject bindable, object oldValue, object newValue) {
            Animate((VisualElement)bindable, (bool)newValue);
        }







        public static readonly BindableProperty ToPointProperty =
            BindableProperty.CreateAttached("ToPoint",
                typeof(Point),
                typeof(Translate),
                Point.Zero);

        public static Point GetToPoint(VisualElement ele) {
            return (Point)ele.GetValue(ToPointProperty);
        }

        public static void SetToPoint(VisualElement ele, Point p) {
            ele.SetValue(ToPointProperty, p);
        }






        private static async Task Animate(VisualElement element, bool isCurrent) {
            if (isCurrent)
                element.IsVisible = true;

            var to = GetToPoint(element);

            await element.TranslateTo(to.X, to.Y, 250, isCurrent ? Easing.CubicIn : Easing.CubicInOut)
                .ContinueWith(t => {
                    if (!isCurrent)
                        element.IsVisible = false;
                });
        }
    }
}
