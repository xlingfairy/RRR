using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class WebBaiduMap : View {

        //public static readonly BindableProperty AKProperty =
        //    BindableProperty.Create("AK",
        //        typeof(string),
        //        typeof(WebBaiduMap),
        //        "",
        //        propertyChanged:AKChanged);

        //private static void AKChanged(BindableObject bindable, object oldValue, object newValue) {

        //}

        public string AK {
            get; set;
            //get {
            //    return (string)this.GetValue(AKProperty);
            //}
            //set {
            //    this.SetValue(AKProperty, value);
            //}
        }
    }
}
