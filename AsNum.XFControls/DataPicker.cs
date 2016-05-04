using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;

namespace AsNum.XFControls {
    public class DataPicker : View {

        private PropertyInfo PI = null;

        #region itemsSource 数据源
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(DataPicker),
                null,
                propertyChanged: ItemsSourceChanged);

        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var picker = (DataPicker)bindable;

            var p = picker.ItemsSource
                .GetType()
                .GetTypeInfo()
                .GenericTypeParameters.First()
                .GetRuntimeProperty(picker.DisplayPath);

            picker.PI = p;
        }
        #endregion

        #region SelectedItem
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(DataPicker),
                null,
                BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged);

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {

        }

        public object SelectedItem {
            get {
                return this.GetValue(SelectedItemProperty);
            }
            set {
                this.SetValue(SelectedItemProperty, value);
            }
        }

        #endregion

        #region
        public string DisplayPath { get; set; }
        #endregion

        public List<string> StringValues {
            get {

                if (this.ItemsSource != null && this.PI != null && !string.IsNullOrWhiteSpace(this.DisplayPath)) {
                    return ((IEnumerable<string>)this.ItemsSource)
                        .Select(i => this.PI.GetValue(i)?.ToString())
                        .ToList();
                }
                else if (this.ItemsSource != null && this.ItemsSource is IEnumerable<string>) {
                    return ((IEnumerable<string>)this.ItemsSource).ToList();
                }

                return new List<string>();
            }
        }

        public int SelectedIndex {
            get {
                if (this.SelectedItem != null && this.PI != null) {
                    var str = this.PI.GetValue(this.SelectedItem)?.ToString();
                    return this.StringValues.IndexOf(str);
                }
                else {
                    return -1;
                }
            }
        }
    }
}
