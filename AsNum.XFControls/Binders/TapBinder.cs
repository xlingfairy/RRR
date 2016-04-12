using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls.Binders {
    public class TapBinder {

        public static readonly BindableProperty CmdProperty =
            BindableProperty.CreateAttached("Cmd",
                typeof(ICommand),
                typeof(TapBinder),
                null,
                propertyChanged: Changed);

        public static void SetCmd(BindableObject view, ICommand cmd) {
            view.SetValue(CmdProperty, cmd);
        }

        public static ICommand GetCmd(BindableObject view) {
            return (ICommand)view.GetValue(CmdProperty);
        }


        public static readonly BindableProperty ParamProperty =
            BindableProperty.CreateAttached("Param",
                typeof(object),
                typeof(TapBinder),
                null,
                propertyChanged: Changed);

        public static void SetParam(BindableObject view, object param) {
            view.SetValue(ParamProperty, param);
        }

        public static object GetParam(BindableObject view) {
            return view.GetValue(ParamProperty);
        }

        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var view = (View)bindable;
            var gesture = (TapGestureRecognizer)view.GestureRecognizers.FirstOrDefault(g => g is TapGestureRecognizer);

            if (gesture == null) {
                gesture = new TapGestureRecognizer();
                view.GestureRecognizers.Add(gesture);
            }
            gesture.Command = GetCmd(view);
            gesture.CommandParameter = GetParam(view);
        }
    }
}
