using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls.Binders {
    public class EditorBinder {

        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.CreateAttached("PlaceHolder",
                typeof(string),
                typeof(EditorBinder),
                null,
                propertyChanged: Changed
                );

        public static string GetPlaceHolder(BindableObject view) {
            return (string)view.GetValue(PlaceHolderProperty);
        }


        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var view = (View)bindable;
            if (view != null) {
                var effect = view.Effects.FirstOrDefault(e => e is EditorEffect);
                if (effect == null) {
                    effect = new EditorEffect();
                    view.Effects.Add(effect);
                }
            }
        }


        class EditorEffect : RoutingEffect {
            public EditorEffect()
                : base("AsNum.EditorEffect") {
            }
        }
    }
}
