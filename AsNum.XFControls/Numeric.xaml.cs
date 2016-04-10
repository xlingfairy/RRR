using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsNum.XFControls {
    public partial class Numeric : ContentView {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value",
                typeof(decimal),
                typeof(Numeric),
                0m,
                defaultBindingMode: BindingMode.TwoWay);


        /// <summary>
        /// 精度
        /// </summary>
        public static readonly BindableProperty PrecisionProperty =
            BindableProperty.Create("Precision",
                typeof(byte),
                typeof(Numeric),
                byte.MinValue);


        public static readonly BindableProperty MinProperty =
            BindableProperty.Create("Min",
                typeof(decimal),
                typeof(Numeric),
                decimal.MinValue);

        public static readonly BindableProperty MaxProperty =
            BindableProperty.Create("Max",
                typeof(decimal),
                typeof(Numeric),
                decimal.MaxValue);

        public static readonly BindableProperty StepProperty =
            BindableProperty.Create("Step",
                typeof(ulong),
                typeof(Numeric),
                ulong.MinValue + 1);

        public decimal Value {
            get {
                return (decimal)this.GetValue(ValueProperty);
            }
            set {
                this.SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// 精度
        /// </summary>
        public byte Precision {
            get {
                return (byte)this.GetValue(PrecisionProperty);
            }
            set {
                this.SetValue(PrecisionProperty, value);
            }
        }

        public decimal Max {
            get {
                return (decimal)this.GetValue(MaxProperty);
            }
            set {
                this.SetValue(MaxProperty, value);
            }
        }

        public decimal Min {
            get {
                return (decimal)this.GetValue(MinProperty);
            }
            set {
                this.SetValue(MinProperty, value);
            }
        }

        public ulong Step {
            get {
                return (ulong)this.GetValue(StepProperty);
            }
            set {
                this.SetValue(StepProperty, value);
            }
        }





        public Numeric() {
            //InitializeComponent();
            this.LoadFromXaml(typeof(Numeric));
            this.BindingContext = this;
        }
    }
}
