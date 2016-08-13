using AsNum.XFControls.Binders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class RadioGroup : ContentView {

        #region SelectedItem
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(RadioGroup),
                null,
                BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged);

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {
            var rg = (RadioGroup)bindable;
            //var item = rg.Items.FirstOrDefault(r => r.Value.Equals(newValue));
            //rg.SelectedCmd.Execute(item);
            rg.UpdateSelected();
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

        #region itemsSource 数据源
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(RadioGroup),
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
            var rg = (RadioGroup)bindable;
            //rg.Items.Clear();
            if (newValue != null) {
                var source = (IEnumerable<object>)newValue;
                rg.Add(source.ToList(), 0);
            }

            rg.UpdateSelected();
        }
        #endregion

        #region Orientation
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation",
                                    typeof(StackOrientation),
                                    typeof(RadioGroup),
                                    StackOrientation.Horizontal,
                                    propertyChanged: OrientationChanged);

        private static void OrientationChanged(BindableObject bindable, object oldValue, object newValue) {
            var rg = (RadioGroup)bindable;
            rg.Container.Orientation = (StackOrientation)newValue;
        }

        public StackOrientation Orientation {
            get {
                return (StackOrientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }
        #endregion

        #region DisplayPath
        public static readonly BindableProperty DisplayPathProperty =
            BindableProperty.Create("DisplayPath",
                                    typeof(string),
                                    typeof(RadioGroup));

        public string DisplayPath {
            get {
                return (string)this.GetValue(DisplayPathProperty);
            }
            set {
                this.SetValue(DisplayPathProperty, value);
            }
        }

        #endregion

        #region RadioSize
        private static readonly BindableProperty RadioSizeProperty =
            BindableProperty.Create("Size",
                                    typeof(double),
                                    typeof(RadioGroup),
                                    25D
                                    );

        public double RadioSize {
            get {
                return (double)this.GetValue(RadioSizeProperty);
            }
            set {
                this.SetValue(RadioSizeProperty, value);
            }
        }
        #endregion

        //public ObservableCollection<Radio> Items {
        //    get;
        //} = new ObservableCollection<Radio>();

        private ICommand SelectedCmd { get; }

        private Radio SelectedRadio = null;

        private StackLayout Container = null;

        public RadioGroup() {
            this.Container = new StackLayout() {
                Orientation = this.Orientation
            };
            this.Content = this.Container;

            this.SelectedCmd = new Command((o) => {
                if (o == null)
                    return;

                var item = (Radio)o;
                if (this.SelectedRadio != null) {
                    this.SelectedRadio.IsSelected = false;
                }
                this.SelectedItem = item.Value;
                this.SelectedRadio = item;
                item.IsSelected = true;
            });

            new NotifyCollectionWrapper(this.ItemsSource,
                add: (datas, idx) => this.Add(datas, idx),
                remove: (datas, idx) => this.Remove(datas, idx),
                reset: () => this.Reset(),
                finished: () => { });
        }

        private void Add(IList datas, int idx) {
            var c = this.Container.Children.Count;

            foreach (var d in datas) {
                var v = this.GetRadio(d);
                if (idx < c)
                    this.Container.Children.Insert(idx++, v);
                else
                    this.Container.Children.Add(v);
            }
        }

        private void Remove(IList datas, int idx) {
            var rms = this.Container.Children.Skip(idx).Take(datas.Count);
            foreach (var rm in rms) {
                this.Container.Children.Remove(rm);
            }
        }

        private void Reset() {
            this.Container.Children.Clear();
            foreach (var d in this.ItemsSource) {
                var v = this.GetRadio(d);
                this.Container.Children.Add(v);
            }
        }

        private Radio GetRadio(object data) {
            Radio item = null;
            if (data is Radio) {
                item = (Radio)data;
            } else {
                item = new Radio();
                item.Value = data;

                if (!string.IsNullOrWhiteSpace(this.DisplayPath)) {
                    //item.SetBinding(Radio.TextProperty, this.DisplayPath);
                    item.Text = Helper.GetProperty<string>(data, this.DisplayPath, "DisplayPath Invalid");
                } else
                    item.Text = data.ToString();
            }

            item.Size = this.RadioSize;

            TapBinder.SetCmd(item, this.SelectedCmd);
            TapBinder.SetParam(item, item);

            return item;
        }

        private void UpdateSelected() {
            var item = this.SelectedItem;
            var radio = this.Container.Children.FirstOrDefault(r => ((Radio)r).Value.Equals(item));
            this.SelectedCmd.Execute(radio);
        }
    }
}
