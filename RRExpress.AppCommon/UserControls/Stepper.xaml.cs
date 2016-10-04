using Xamarin.Forms;

namespace RRExpress.AppCommon.UserControls {

    /// <summary>
    /// 控件与MVVM设计原则：
    /// MVVM 是给使用控件的人用的，不是给写控件的人用的
    /// 如果控件里使用了 MVVM 会造成 BindingContext 错乱
    /// </summary>
    public partial class Stepper : ContentView {


        #region Min
        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly BindableProperty MinProperty =
            BindableProperty.Create("Min",
                typeof(double),
                typeof(Stepper),
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
                typeof(Stepper),
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
                typeof(Stepper),
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
                typeof(Stepper),
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
                typeof(Stepper),
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


        public Stepper() {
            InitializeComponent();
        }


        private void Update() {
            this.lbl.Text = this.Value.ToString(this.Format ?? "");
            this.btnReduce.IsEnabled = this.Value > this.Min;
            this.btnIncrease.IsEnabled = this.Value < this.Max;
        }
    }
}
