using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class Radio : ContentView {


        #region value
        public static BindableProperty ValueProperty =
            BindableProperty.Create("Value",
                typeof(object),
                typeof(Radio));

        public object Value {
            get {
                return this.GetValue(ValueProperty);
            }
            set {
                this.SetValue(ValueProperty, value);
            }
        }
        #endregion

        #region isSelected
        public static BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected",
                typeof(bool),
                typeof(Radio),
                false,
                BindingMode.TwoWay,
                propertyChanged: IsSelectedChanged
                );

        private static void IsSelectedChanged(BindableObject bindable, object oldValue, object newValue) {
            var radio = (Radio)bindable;
            var source = (bool)newValue ? Checked : Unchecked;
            radio.Icon.Source = source; //new BytesImageSource(datas);
        }

        public bool IsSelected {
            get {
                return (bool)this.GetValue(IsSelectedProperty);
            }
            set {
                this.SetValue(IsSelectedProperty, value);
            }
        }
        #endregion

        #region Text
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text",
                                    typeof(string),
                                    typeof(Radio),
                                    "",
                                    propertyChanged: TextChanged
                                    );

        private static void TextChanged(BindableObject bindable, object oldValue, object newValue) {
            var radio = (Radio)bindable;
            radio.Lbl.Text = (string)newValue;
        }

        public string Text {
            get {
                return (string)this.GetValue(TextProperty);
            }
            set {
                this.SetValue(TextProperty, value);
            }
        }
        #endregion

        #region TextAlignment
        public static readonly BindableProperty TextAlignmentProperty =
            BindableProperty.Create("TextAlignment",
                typeof(TextAlignment),
                typeof(Radio),
                TextAlignment.Start
                );

        public TextAlignment TextAlignment {
            get {
                return (TextAlignment)this.GetValue(TextAlignmentProperty);
            }
            set {
                this.SetValue(TextAlignmentProperty, value);
            }
        }
        #endregion

        #region Size
        internal static readonly BindableProperty SizeProperty =
            BindableProperty.Create("Size",
                                    typeof(double),
                                    typeof(Radio),
                                    25D,
                                    propertyChanged: IconSizeChanged);

        internal double Size {
            get {
                return (double)this.GetValue(SizeProperty);
            }
            set {
                this.SetValue(SizeProperty, value);
            }
        }

        private static void IconSizeChanged(BindableObject bindable, object oldValue, object newValue) {
            var chk = (Radio)bindable;
            chk.Icon.WidthRequest = chk.Icon.HeightRequest = (double)newValue;

        }
        #endregion

        #region ShowRadio
        public static readonly BindableProperty ShowRadioProperty =
            BindableProperty.Create("ShowRadio",
                                    typeof(bool),
                                    typeof(Radio),
                                    true
                                    );

        public bool ShowRadio {
            get {
                return (bool)this.GetValue(ShowRadioProperty);
            }
            set {
                this.SetValue(ShowRadioProperty, value);
            }
        }
        #endregion

        private static readonly ImageSource Checked;
        private static readonly ImageSource Unchecked;

        static Radio() {
            Unchecked = ImageSource.FromResource("AsNum.XFControls.Imgs.Radio-Unchecked.png"); //GetImg("AsNum.XFControls.Imgs.Radio-Unchecked.png");
            Checked = ImageSource.FromResource("AsNum.XFControls.Imgs.Radio-Checked.png"); //GetImg("AsNum.XFControls.Imgs.Radio-Checked.png");
        }

        //private static byte[] GetImg(string imgFile) {
        //    var asm = typeof(Radio).GetTypeInfo().Assembly;
        //    using (var stm = asm.GetManifestResourceStream(imgFile)) {
        //        return stm.GetBytes();
        //    }
        //}


        private Image Icon;
        private Label Lbl;

        public Radio() {
            var layout = new StackLayout() {
                Orientation = StackOrientation.Horizontal
            };

            this.Content = layout;

            this.Icon = new Image() {
                Source = Unchecked, //new BytesImageSource(Unchecked),
                WidthRequest = this.Size,
                HeightRequest = this.Size
            };
            this.Icon.SetBinding(Image.IsVisibleProperty, new Binding("ShowRadio", source: this));
            layout.Children.Add(this.Icon);

            this.Lbl = new Label();
            this.Lbl.SetBinding(Label.HorizontalTextAlignmentProperty, new Binding("TextAlignment", source: this));
            this.Lbl.Text = this.Text;
            layout.Children.Add(this.Lbl);

        }
    }
}
