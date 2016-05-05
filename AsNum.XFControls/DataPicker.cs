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
        public string DisplayFormat { get; set; }
        //public Color TextColor { get; set; }
        //public Color DividerColor { get; set; }
        #endregion

        public IList<string> StringValues {
            get {

                var lst = new List<string>();

                if (this.ItemsSource != null && !string.IsNullOrWhiteSpace(this.DisplayPath)) {

                    foreach (var d in this.ItemsSource) {
                        lst.Add(Helper.GetProperty(d, this.DisplayPath)?.ToString());
                    }
                }
                else if (this.ItemsSource != null) {
                    foreach (var d in this.ItemsSource)
                        lst.Add(d.ToString());
                }

                return lst;
            }
        }

        public int SelectedIndex {
            get {
                if (this.SelectedItem != null) {
                    var str = Helper.GetProperty(this.SelectedItem, this.DisplayPath)?.ToString();
                    return this.StringValues.IndexOf(str);
                }
                else {
                    return -1;
                }
            }
        }
    }
}
