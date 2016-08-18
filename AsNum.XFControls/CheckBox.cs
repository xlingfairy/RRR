using AsNum.XFControls.Binders;
using NGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class CheckBox : ContentView {

        #region IsChecked
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create("Checked",
                typeof(bool),
                typeof(CheckBox),
                false,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: CheckedChanged
                );


        public bool Checked {
            get {
                return (bool)this.GetValue(CheckedProperty);
            }
            set {
                this.SetValue(CheckedProperty, value);
            }
        }

        private static void CheckedChanged(BindableObject bindable, object oldValue, object newValue) {
            var chk = (CheckBox)bindable;
            var source = chk.Checked ? CheckedImg : UnCheckedImg;
            chk.Icon.Source = source;// new BytesImageSource(datas);
        }
        #endregion

        #region IsShowLabel
        public static readonly BindableProperty ShowLabelProperty =
            BindableProperty.Create("ShowLabel",
                typeof(bool),
                typeof(CheckBox),
                false
                );


        public bool ShowLabel {
            get {
                return (bool)this.GetValue(ShowLabelProperty);
            }
            set {
                this.SetValue(ShowLabelProperty, value);
            }
        }

        #endregion

        #region Text
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text",
                typeof(string),
                typeof(CheckBox),
                null,
                defaultBindingMode: BindingMode.TwoWay
                );


        public string Text {
            get {
                return (string)this.GetValue(TextProperty);
            }
            set {
                this.SetValue(TextProperty, value);
            }
        }
        #endregion


        #region Size
        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create("Size",
                                    typeof(double),
                                    typeof(CheckBox),
                                    25D,
                                    propertyChanged: IconSizeChanged);

        public double Size {
            get {
                return (double)this.GetValue(SizeProperty);
            }
            set {
                this.SetValue(SizeProperty, value);
            }
        }

        private static void IconSizeChanged(BindableObject bindable, object oldValue, object newValue) {
            var chk = (CheckBox)bindable;
            chk.Icon.WidthRequest = chk.Icon.HeightRequest = (double)newValue;

        }
        #endregion

        //private static readonly Graphic CheckedG;
        //private static readonly Graphic UnCheckedG;

        private static readonly ImageSource CheckedImg;
        private static readonly ImageSource UnCheckedImg;

        static CheckBox() {
            UnCheckedImg = ImageSource.FromResource("AsNum.XFControls.Imgs.Checkbox-Unchecked.png");// GetImg("AsNum.XFControls.Imgs.Checkbox-Unchecked.png");
            CheckedImg = ImageSource.FromResource("AsNum.XFControls.Imgs.Checkbox-Checked.png");// GetImg("AsNum.XFControls.Imgs.Checkbox-Checked.png");
        }

        //private static Graphic GetGraphic(string svgFile) {
        //    var asm = typeof(CheckBox).GetTypeInfo().Assembly;
        //    var stmReader = new StreamReader(asm.GetManifestResourceStream(svgFile));
        //    var svgReader = new SvgReader(stmReader);
        //    return svgReader.Graphic;
        //}

        //private static byte[] GetImg(string imgFile) {
        //    var asm = typeof(CheckBox).GetTypeInfo().Assembly;
        //    using (var stm = asm.GetManifestResourceStream(imgFile)) {
        //        return stm.GetBytes();
        //    }
        //}

        private readonly Grid Grid;
        private readonly Label Label;
        //private readonly NControlView Icon;
        private readonly Image Icon;
        private ICommand TapCmd { get; }

        public CheckBox() {
            this.TapCmd = new Command(() => {
                this.Checked = !this.Checked;
            });

            var cols = new ColumnDefinitionCollection();
            cols.Add(new ColumnDefinition() { Width = GridLength.Auto });
            cols.Add(new ColumnDefinition() { Width = GridLength.Auto });
            this.Grid = new Grid() {
                ColumnDefinitions = cols,
            };
            //this.Grid.GestureRecognizers.Add(new TapGestureRecognizer() {
            //    Command = this.TapCmd
            //});
            Binders.TapBinder.SetCmd(this.Grid, this.TapCmd);

            this.Content = this.Grid;

            this.Label = new Label() { BindingContext = this };
            this.Label.SetBinding(Label.TextProperty, "Text");
            this.Label.SetBinding(Label.IsVisibleProperty, "ShowLabel");
            this.Label.SetValue(Grid.ColumnProperty, 1);
            //TapBinder.SetCmd(this.Label, this.TapCmd);
            this.Grid.Children.Add(this.Label);


            //this.Icon = new NControlView() {
            //    DrawingFunction = (canvas, rect) => this.DrawIcon(canvas, rect)
            //};
            this.Icon = new Image() {
                WidthRequest = this.Size,
                HeightRequest = this.Size,
                Source = UnCheckedImg // new BytesImageSource(UnCheckedImg)
            };
            this.Icon.SetValue(Grid.ColumnProperty, 0);
            //TapBinder.SetCmd(this.Label, this.TapCmd);
            this.Grid.Children.Add(this.Icon);
        }

        //private void DrawIcon(ICanvas canvas, Rect rect) {
        //    canvas.FillRectangle(rect, new SolidBrush(NGraphics.Color.FromRGB(0, 0, 0, 0)));
        //    //canvas.DrawText("Test", rect, new NGraphics.Font(), NGraphics.TextAlignment.Center, NGraphics.Color.FromRGB(0xff, 0, 0));

        //    if (this.Checked) {
        //        CheckedG.Draw(canvas);
        //    } else {
        //        UnCheckedG.Draw(canvas);
        //    }
        //}
    }
}
