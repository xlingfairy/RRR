using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class TabPageView : ContentView {

        #region IsSelected
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected",
                typeof(bool),
                typeof(TabPageView),
                false
                );

        public bool IsSelected {
            get {
                return (bool)this.GetValue(IsSelectedProperty);
            }
            set {
                this.SetValue(IsSelectedProperty, value);
            }
        }
        #endregion

        #region Index
        public static BindablePropertyKey IndexPropertyKey =
            BindableProperty.CreateReadOnly("Index",
                typeof(int),
                typeof(TabPageView),
                0);

        public int Index {
            get {
                return (int)this.GetValue(IndexPropertyKey.BindableProperty);
            }
            set {
                this.SetValue(IndexPropertyKey.BindableProperty, value);
            }
        }
        #endregion

        #region Header
        public static readonly BindableProperty HeaderProperty =
            BindableProperty.Create("Header",
                typeof(View),
                typeof(TabPageView));

        public View Header {
            get {
                return (View)this.GetValue(HeaderProperty);
            }
            set {
                this.SetValue(HeaderProperty, value);
            }
        }
        #endregion
    }
}
