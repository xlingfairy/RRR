using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.UserControls {
    public partial class Stepper : ContentView {

        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly BindableProperty MinProperty =
            BindableProperty.Create("Min",
                typeof(double),
                typeof(Stepper),
                double.MinValue,
                propertyChanged: Changed);


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


        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly BindableProperty MaxProperty =
            BindableProperty.Create("Max",
                typeof(double),
                typeof(Stepper),
                double.MaxValue,
                propertyChanged: Changed);

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


        /// <summary>
        /// 步长
        /// </summary>
        public static readonly BindableProperty StepProperty =
            BindableProperty.Create("Step",
                typeof(double),
                typeof(Stepper),
                1d,
                propertyChanged: Changed);


        /// <summary>
        /// 步长
        /// </summary>
        public double Step {
            get {
                return (double)this.GetValue(StepProperty);
            }
            set {
                this.SetValue(StepProperty, value);
            }
        }

        /// <summary>
        /// 当前值
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value",
                typeof(double),
                typeof(Stepper),
                0d,
                propertyChanged: Changed);

        /// <summary>
        /// 当前值
        /// </summary>
        public double Value {
            get {
                return (double)this.GetValue(ValueProperty);
            }
            set {
                this.SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// 格式化后的值
        /// </summary>
        public string StringValue {
            get {
                return this.Value.ToString(this.Format ?? "");
            }
        }

        /// <summary>
        /// 格式
        /// </summary>
        public static readonly BindableProperty FormatProperty =
            BindableProperty.Create("Format",
                typeof(string),
                typeof(Stepper),
                "0");

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


        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var stepper = (Stepper)bindable;
            stepper.Check();
        }

        /// <summary>
        /// 增加
        /// </summary>
        public ICommand IncreaseCmd {
            get;
        }

        /// <summary>
        /// 减少
        /// </summary>
        public ICommand ReduceCmd {
            get;
        }



        /// <summary>
        /// 是否可减少
        /// </summary>
        public bool CanReduce {
            get; set;
        } = true;

        /// <summary>
        /// 是否可增加
        /// </summary>
        public bool CanIncrease {
            get; set;
        } = true;


        public Stepper() {
            InitializeComponent();

            this.IncreaseCmd = new Command(() => {
                this.Value += this.Step;
                this.Check();
            });

            this.ReduceCmd = new Command(() => {
                this.Value -= this.Step;
                this.Check();
            });

            this.BindingContext = this;
        }



        private void Check() {
            if (this.Value <= this.Min) {
                this.Value = this.Min;
                this.CanReduce = false;
            } else
                this.CanReduce = true;

            if (this.Value >= this.Max) {
                this.Value = this.Max;
                this.CanIncrease = false;
            } else
                this.CanIncrease = true;


            this.OnPropertyChanged("CanIncrease");
            this.OnPropertyChanged("CanReduce");

            if (this.Step == 0)
                this.Step = 1;
        }


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals("Value") || propertyName.Equals("Format")) {
                this.OnPropertyChanged("StringValue");
            }
        }
    }
}
