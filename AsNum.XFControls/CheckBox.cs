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

        public event EventHandler CheckChanged;

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

        #region CheckChangedCmd
        public static readonly BindableProperty CheckChangedCmdProperty =
            BindableProperty.Create("CheckChangedCmd",
                typeof(ICommand),
                typeof(CheckBox)
                );

        public ICommand CheckChangedCmd {
            get {
                return (ICommand)this.GetValue(CheckChangedCmdProperty);
            }
            set {
                this.SetValue(CheckChangedCmdProperty, value);
            }
        }
        #endregion

        private static readonly ImageSource CheckedImg;
        private static readonly ImageSource UnCheckedImg;

        static CheckBox() {
            UnCheckedImg = ImageSource.FromResource("AsNum.XFControls.Imgs.Checkbox-Unchecked.png");// GetImg("AsNum.XFControls.Imgs.Checkbox-Unchecked.png");
            CheckedImg = ImageSource.FromResource("AsNum.XFControls.Imgs.Checkbox-Checked.png");// GetImg("AsNum.XFControls.Imgs.Checkbox-Checked.png");
        }

        private readonly Grid Grid;
        private readonly Label Label;
        private readonly Image Icon;
        private ICommand TapCmd { get; }

        public CheckBox() {
            this.TapCmd = new Command(() => {
                this.Checked = !this.Checked;

                if (this.CheckChanged != null)
                    this.CheckChanged.Invoke(this, new EventArgs());

                if (this.CheckChangedCmd != null && this.CheckChangedCmd.CanExecute(this.Checked))
                    this.CheckChangedCmd.Execute(this.Checked);
            });

            var cols = new ColumnDefinitionCollection();
            cols.Add(new ColumnDefinition() { Width = GridLength.Auto });
            cols.Add(new ColumnDefinition() { Width = GridLength.Auto });
            this.Grid = new Grid() {
                ColumnDefinitions = cols,
            };

            Binders.TapBinder.SetCmd(this.Grid, this.TapCmd);

            this.Content = this.Grid;

            this.Label = new Label() { BindingContext = this };
            this.Label.SetBinding(Label.TextProperty, "Text");
            this.Label.SetBinding(Label.IsVisibleProperty, "ShowLabel");
            this.Label.SetValue(Grid.ColumnProperty, 1);

            this.Grid.Children.Add(this.Label);

            this.Icon = new Image() {
                WidthRequest = this.Size,
                HeightRequest = this.Size,
                Source = UnCheckedImg // new BytesImageSource(UnCheckedImg)
            };
            this.Icon.SetValue(Grid.ColumnProperty, 0);
            this.Grid.Children.Add(this.Icon);
        }
    }
}
