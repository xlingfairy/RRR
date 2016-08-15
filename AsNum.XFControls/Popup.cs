using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class Popup : ContentView {

        public static readonly BindableProperty ViewProperty =
            BindableProperty.Create("View",
                typeof(Layout),
                typeof(Popup)
                );

        public Layout View {
            get {
                return (Layout)this.GetValue(ViewProperty);
            }
            set {
                this.SetValue(ViewProperty, value);
            }
        }
    }
}
