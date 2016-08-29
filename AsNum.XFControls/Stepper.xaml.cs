using AsNum.XFControls.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AsNum.XFControls {
    public partial class Stepper : ContentView {


        #region Min
        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly BindableProperty MinProperty =
            BindableProperty.Create("Min",
                typeof(double),
                typeof(Xamarin.Forms.Stepper),
                double.MinValue);


        /// <summary>
        /// 最小值
        /// </summary>
        public double Min {
            get {
                return (double)this.GetValue(MinProperty);
            }
            set {
                this.SetValue(MinProperty, value);
            }
        }
        #endregion

        #region max
        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly BindableProperty MaxProperty =
            BindableProperty.Create("Max",
                typeof(double),
                typeof(Xamarin.Forms.Stepper),
                double.MaxValue);

        /// <summary>
        /// 最大值
        /// </summary>
        public double Max {
            get {
                return (double)this.GetValue(MaxProperty);
            }
            set {
                this.SetValue(MaxProperty, value);
            }
        }
        #endregion

        #region Step
        /// <summary>
        /// 步长
        /// </summary>
        public static readonly BindableProperty StepProperty =
            BindableProperty.Create("Step",
                typeof(double),
                typeof(Xamarin.Forms.Stepper),
                1d
                );


        /// <summary>
        /// 步长
        /// </summary>
        public double Step {
            get {
                return (double)this.GetValue(StepProperty);
            }
            set {
                if (value < 1)
                    value = 1;
                this.SetValue(StepProperty, value);
            }
        }
        #endregion;

        #region value
        /// <summary>
        /// 当前值
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value",
                typeof(double),
                typeof(Xamarin.Forms.Stepper),
                0d,
                BindingMode.TwoWay,
                propertyChanged: ValueChanged);

        /// <summary>
        /// 当前值
        /// </summary>
        public double Value {
            get {
                return (double)this.GetValue(ValueProperty);
            }
            set {
                if (value < this.Min)
                    value = this.Min;
                if (value > this.Max)
                    value = this.Max;
                this.SetValue(ValueProperty, value);
            }
        }

        private static void ValueChanged(BindableObject bindable, object oldValue, object newValue) {
            var stepper = (Stepper)bindable;
            stepper.Update();
        }

        #endregion

        #region format
        /// <summary>
        /// 格式
        /// </summary>
        public static readonly BindableProperty FormatProperty =
            BindableProperty.Create("Format",
                typeof(string),
                typeof(Xamarin.Forms.Stepper),
                "0",
                propertyChanged: FmtChanged);

        /// <summary>
        /// 格式
        /// </summary>
        public string Format {
            get {
                return (string)this.GetValue(FormatProperty);
            }
            set {
                this.SetValue(FormatProperty, value);
            }
        }

        private static void FmtChanged(BindableObject bindable, object oldValue, object newValue) {
            var stepper = (Stepper)bindable;
            stepper.lbl.Text = stepper.Value.ToString(stepper.Format ?? "");
        }
        #endregion


        #region Color
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color",
                typeof(Color),
                typeof(Stepper),
                Color.FromHex("#cccccc")
                );

        public Color Color {
            get {
                return (Color)this.GetValue(ColorProperty);
            }
            set {
                this.SetValue(ColorProperty, value);
            }
        }
        #endregion

        public Stepper() {
            InitializeComponent();

            TapBinder.SetCmd(this.btnIncrease, new Command(() => {
                if (this.btnIncrease.IsEnabled) {
                    this.Value += this.Step;
                    this.Update();
                }
            }));

            TapBinder.SetCmd(this.btnReduce, new Command(() => {
                if (this.btnReduce.IsEnabled) {
                    this.Value -= this.Step;
                    this.Update();
                }
            }));

            this.Update();
        }


        private void Update() {
            this.lbl.Text = this.Value.ToString(this.Format ?? "");
            this.btnReduce.IsEnabled = this.Value > this.Min;
            this.btnIncrease.IsEnabled = this.Value < this.Max;
        }
    }
}
