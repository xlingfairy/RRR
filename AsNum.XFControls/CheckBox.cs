using NControl.Abstractions;
using NGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class CheckBox : NControlView {

        #region IsChecked
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create("Checked",
                typeof(bool),
                typeof(CheckBox),
                false,
                BindingMode.TwoWay,
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
            chk.Invalidate();
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
                propertyChanged: LabelChanged);


        public string Text {
            get {
                return (string)this.GetValue(TextProperty);
            }
            set {
                this.SetValue(TextProperty, value);
            }
        }

        private static void LabelChanged(BindableObject bindable, object oldValue, object newValue) {

        }
        #endregion


        private static readonly Graphic CheckedG;
        private static readonly Graphic UnCheckedG;

        static CheckBox() {
            CheckedG = GetGraphic("AsNum.XFControls.Svgs.plus.svg");
            UnCheckedG = GetGraphic("AsNum.XFControls.Svgs.minus.svg");
        }

        private static Graphic GetGraphic(string svgFile) {
            var asm = typeof(CheckBox).GetTypeInfo().Assembly;
            var stmReader = new StreamReader(asm.GetManifestResourceStream(svgFile));
            var svgReader = new SvgReader(stmReader);
            return svgReader.Graphic;
        }

        private readonly Grid Grid;
        private readonly Label Label;

        public CheckBox() {
            var cols = new ColumnDefinitionCollection();
            cols.Add(new ColumnDefinition() { Width = GridLength.Auto });
            cols.Add(new ColumnDefinition() { Width = GridLength.Auto });
            this.Grid = new Grid() {
                ColumnDefinitions = cols
            };

            this.Content = this.Grid;

            this.Label = new Label();
            this.Label.SetValue(Label.TextProperty, this.Text);
            this.Label.SetValue(Label.IsVisibleProperty, this.ShowLabel);
            this.Label.SetValue(Grid.ColumnProperty, 1);

            this.Grid.Children.Add(this.Label);

            //this.DrawingFunction = (canvas, rect) => this.DrawSvg(canvas, rect);
        }

        public override void Draw(ICanvas canvas, Rect rect) {
            base.Draw(canvas, rect);

            canvas.FillRectangle(rect, new SolidBrush(NGraphics.Color.FromRGB(0, 0, 0, 0)));

            canvas.DrawText("Test", rect, new NGraphics.Font(), NGraphics.TextAlignment.Center, NGraphics.Color.FromRGB(0xff, 0, 0));

            if (this.Checked) {
                CheckedG.Draw(canvas);
            }
            else {
                UnCheckedG.Draw(canvas);
            }
        }
    }
}
