using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class Repeater : WrapLayout {

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(Repeater),
                null
                );

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(Repeater),
                null,
                BindingMode.OneWay,
                propertyChanged: ItemsChanged);


        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(Repeater),
                null,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged
                );











        public DataTemplate ItemTemplate {
            get {
                return this.GetValue(ItemTemplateProperty) as DataTemplate;
            }

            set {
                this.SetValue(ItemTemplateProperty, value);
            }
        }

        public IEnumerable ItemsSource {
            get {
                return this.GetValue(ItemsSourceProperty) as IEnumerable;
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public object SelectedItem {
            get {
                return this.GetValue(SelectedItemProperty);
            }
            set {
                this.SetValue(SelectedItemProperty, value);
            }
        }



        private Command TapCmd { get; }


        public Repeater() {
            this.TapCmd = new Command(o => {
                this.SelectedItem = o;
            });
        }



        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {
            var rp = (Repeater)bindable;
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue) {
            var rp = (Repeater)bindable;
            var v = newValue as INotifyCollectionChanged;
            if (v != null)
                rp.InitCollection(v);
            else {
                rp.RemoveAll();
                rp.Add((IEnumerable)newValue);
            }
        }

        private void InitCollection(INotifyCollectionChanged datas) {
            datas.CollectionChanged += Datas_CollectionChanged;
        }

        private void Datas_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Remove(e.OldItems, e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Move:
                    Debugger.Break();
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Debugger.Break();
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.RemoveAll();
                    this.Add(this.ItemsSource);
                    break;
            }
        }

        private void Add(IEnumerable datas, int startIdx = 0) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                var v = this.ItemTemplate.CreateContent() as View;
                v.GestureRecognizers.Add(new TapGestureRecognizer() {
                    Command = this.TapCmd,
                    CommandParameter = d
                });
                v.BindingContext = d;
                this.Children.Insert(startIdx++, v);
                v.Parent = this;
            }
        }

        private void Remove(IList datas, int startIdx) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                this.Children.RemoveAt(startIdx++);
            }
        }

        private void RemoveAll() {
            var children = this.Children.ToList();
            foreach (var c in children)
                this.Children.Remove(c);
        }

    }
}
