using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.UserControls {
    public partial class Stepper : ContentView {

        public static readonly BindableProperty MinProperty =
            BindableProperty.Create("Min",
                typeof(double),
                typeof(Stepper),
                double.MinValue,
                propertyChanged: Changed);

        public double Min {
            get {
                return (double)this.GetValue(MinProperty);
            }
            set {
                this.SetValue(MinProperty, value);
            }
        }

        public static readonly BindableProperty MaxProperty =
            BindableProperty.Create("Max",
                typeof(double),
                typeof(Stepper),
                double.MaxValue,
                propertyChanged: Changed);

        public double Max {
            get {
                return (double)this.GetValue(MaxProperty);
            }
            set {
                this.SetValue(MaxProperty, value);
            }
        }


        public static readonly BindableProperty StepProperty =
            BindableProperty.Create("Step",
                typeof(double),
                typeof(Stepper),
                1d,
                propertyChanged: Changed);


        public double Step {
            get {
                return (double)this.GetValue(StepProperty);
            }
            set {
                this.SetValue(StepProperty, value);
            }
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value",
                typeof(double),
                typeof(Stepper),
                0d,
                propertyChanged: Changed);

        public double Value {
            get {
                return (double)this.GetValue(ValueProperty);
            }
            set {
                this.SetValue(ValueProperty, value);
            }
        }

        public static readonly BindableProperty FormatProperty =
            BindableProperty.Create("Format",
                typeof(string),
                typeof(Stepper),
                "{0}");

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


        public ICommand IncreaseCmd {
            get;
        }

        public ICommand ReduceCmd {
            get;
        }

        public string FormatedValue {
            get; set;
        }

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
            if (this.Value < this.Min) {
                this.Value = this.Min;
            }
            if (this.Value > this.Max) {
                this.Value = this.Max;
            }

            if (this.Step == 0)
                this.Step = 1;
        }
    }
}
