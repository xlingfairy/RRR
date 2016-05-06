using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class Marquee : AbsoluteLayout {

        #region itemsSource 数据源
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(object),
                typeof(Marquee),
                null,
                propertyChanged: ItemsSourceChanged);

        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, Enumerable.Empty<object>());
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (Marquee)bindable;
            tv.UpdateChildren();

            if (newValue is INotifyCollectionChanged) {
                var newCollection = (INotifyCollectionChanged)newValue;
                tv.InitCollection(newCollection);
            }
        }
        #endregion

        #region ItemTemplate 数据模板
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(Marquee),
                null);

        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set {
                SetValue(ItemTemplateProperty, value);
            }
        }
        #endregion


        private int _current = 0;
        private int Current {
            get {
                return this._current;
            }
            set {
                this._current = value < 0 ? 0 : value >= this.Children.Count ? 0 : value;
            }
        }

        public Marquee() {
            this.ChildAdded += Marquee_ChildAdded;
            this.Loop();
        }

        private void Animate(View view, bool isCurrent) {
            if (isCurrent)
                view.IsVisible = true;

            view.ScaleTo(isCurrent ? 1 : 0.1)
                .ContinueWith((t) => {
                    if (!isCurrent) {
                        view.IsVisible = false;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Loop() {
            Device.StartTimer(TimeSpan.FromSeconds(3), () => {
                if (this.Children.Count > 0) {
                    var ele = this.Children[this.Current];
                    this.Animate(ele, false);

                    this.Current++;

                    ele = this.Children[this.Current];
                    this.Animate(ele, true);
                }
                return true;
            });
        }

        private void Marquee_ChildAdded(object sender, ElementEventArgs e) {
            this.InitChildView((View)e.Element);
        }

        private void UpdateChildren() {
            this.Children.Clear();
            if (this.ItemsSource == null)
                return;

            var source = this.ItemsSource.Cast<object>();
            foreach (var d in this.ItemsSource) {
                var view = this.GetChildView(d);
                this.Children.Add(view);
            }
        }

        private View GetChildView(object data) {
            View view = null;
            if (this.ItemTemplate != null) {
                if (this.ItemTemplate != null)
                    view = (View)this.ItemTemplate.CreateContent();

                if (view != null) {
                    view.BindingContext = data;
                }
            }

            if (view == null)
                view = new Label() { Text = "111" };

            return view;
        }

        private void InitChildView(View view) {
            view.Scale = 0.1;
            view.IsVisible = false;
            view.VerticalOptions = LayoutOptions.Center;
            AbsoluteLayout.SetLayoutBounds(view, new Rectangle(0, 0.5, view.Width, view.Height));
            AbsoluteLayout.SetLayoutFlags(view, AbsoluteLayoutFlags.PositionProportional);
        }

        private void InitCollection(INotifyCollectionChanged collection) {
            if (collection != null)
                collection.CollectionChanged += Collection_CollectionChanged;
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    this.InsertChildren(e.NewItems, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.RemoveChildren(e.OldItems, e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Move:
                    Debugger.Break();
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Debugger.Break();
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.UpdateChildren();
                    break;
            }
        }

        /// <summary>
        /// 数据源更新(插入)
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="startIdx"></param>
        private void InsertChildren(IEnumerable datas, int startIdx = 0) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                var view = this.GetChildView(d);

                this.Children.Insert(startIdx, view);
                startIdx++;
            }
        }

        /// <summary>
        /// 数据源更新(删除)
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="startIdx"></param>
        private void RemoveChildren(IList datas, int startIdx) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                this.Children.RemoveAt(startIdx);
                startIdx++;
            }
        }
    }
}
