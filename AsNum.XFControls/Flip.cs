using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    [ContentProperty("Children")]
    public class Flip : Layout {

        #region events
        /// <summary>
        /// 下一侦
        /// </summary>
        public event EventHandler NextRequired;

        /// <summary>
        /// 请求指定侦
        /// </summary>
        public event EventHandler<IndexRequestEventArgs> IndexRequired;


        public class IndexRequestEventArgs : EventArgs {

            public int Index { get; }

            public IndexRequestEventArgs(int idx) {
                this.Index = idx;
            }
        }

        #endregion


        #region itemsSource
        /// <summary>
        /// 源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(Flip),
                null,
                BindingMode.OneWay,
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
            var flip = (Flip)bindable;
            flip.CalcChild();

            var wrapper = new NotifyCollectionWrapper(newValue,
                add: (datas, idx) => flip.Add(datas, idx),
                remove: (datas, idx) => flip.Remove(datas, idx),
                reset: () => flip.Reset(),
                finished: () => flip.AA()
                );
        }

        #endregion

        #region orientation
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(
                "Orientation",
                typeof(ScrollOrientation),
                typeof(Flip),
                ScrollOrientation.Horizontal);

        public ScrollOrientation Orientation {
            get {
                return (ScrollOrientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }

        #endregion


        #region template
        /// <summary>
        /// 条目模板
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                "ItemTemplate",
                typeof(DataTemplate),
                typeof(Flip),
                null
                );

        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)this.GetValue(ItemTemplateProperty);
            }
            set {
                this.SetValue(ItemTemplateProperty, value);
            }
        }

        #endregion

        #region autoplay
        /// <summary>
        /// 是否自动播放
        /// </summary>
        public static readonly BindableProperty AutoPlayProperty =
            BindableProperty.Create(
                    "AutoPlay",
                    typeof(bool),
                    typeof(Flip),
                    false,
                    propertyChanged: AutoPlayChanged
                );

        /// <summary>
        /// 
        /// </summary>
        public bool AutoPlay {
            get {
                return (bool)this.GetValue(AutoPlayProperty);
            }
            set {
                this.SetValue(AutoPlayProperty, value);
            }
        }

        private static void AutoPlayChanged(BindableObject bindable, object oldValue, object newValue) {
            var flip = (Flip)bindable;
            if ((bool)newValue) {
                flip.Play();
            } else {
                flip.Stop();
            }
        }

        #endregion

        #region interval
        /// <summary>
        /// 播放间隔
        /// </summary>
        public static readonly BindableProperty IntervalProperty =
            BindableProperty.Create("Interval",
                typeof(int),
                typeof(Flip),
                2000);

        /// <summary>
        /// MilliSecond
        /// </summary>
        public int Interval {
            get {
                return (int)this.GetValue(IntervalProperty);
            }
            set {
                this.SetValue(IntervalProperty, value);
            }
        }

        #endregion

        #region ShowIndicator
        /// <summary>
        /// 是否显示指示点
        /// </summary>
        public static readonly BindableProperty ShowIndicatorProperty =
            BindableProperty.Create(
                    "ShowIndicator",
                    typeof(bool),
                    typeof(Flip),
                    true
                );

        public bool ShowIndicator {
            get {
                return (bool)this.GetValue(ShowIndicatorProperty);
            }
            set {
                this.SetValue(ShowIndicatorProperty, value);
            }
        }

        #endregion

        #region Current
        /// <summary>
        /// 当前侦序号
        /// </summary>
        public static readonly BindableProperty CurrentProperty =
            BindableProperty.Create("Current",
                typeof(int),
                typeof(Flip),
                0,
                propertyChanged: CurrentChanged,
                defaultBindingMode: BindingMode.TwoWay
                );

        public int Current {
            get {
                return (int)this.GetValue(CurrentProperty);
            }
            set {
                this.SetValue(CurrentProperty, value);
            }
        }

        private static void CurrentChanged(BindableObject bindable, object oldValue, object newValue) {
            var flip = (Flip)bindable;
            if (flip.IndexRequired != null) {
                flip.IndexRequired.Invoke(flip, new IndexRequestEventArgs((int)newValue));
            }
        }

        #endregion




        public IList<View> Children {
            get;
            private set;
        } = new List<View>();

        private void AA() {
            //this.InvalidateMeasure();
            //this.ForceLayout();
            this.UpdateChildrenLayout();
        }

        private void Reset() {
            this.Children.Clear();
            this.CalcChild();
        }

        private void Remove(IList datas, int idx) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                this.Children.RemoveAt(idx++);
            }
        }

        private void Add(IList datas, int idx) {
            foreach (var d in datas) {
                var view = this.CreateChild(d);
                this.Children.Insert(idx, view);
            }
        }


        private View CreateChild(object data) {
            var view = (View)this.ItemTemplate.CreateContent();
            view.BindingContext = data;
            view.Parent = this;
            return view;
        }


        private void CalcChild() {
            if (this.ItemsSource == null || this.ItemTemplate == null)
                return;

            foreach (var o in this.ItemsSource) {
                var view = this.CreateChild(o);
                this.Children.Add(view);
            }
        }


        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);

            foreach (var c in this.Children) {
                if (c.Parent == null)
                    c.Parent = this;
                c.Layout(new Rectangle(0, 0, width, height));
            }
        }

        public void Play() {
            this.AutoPlay = true;
            this.InnerPlay();
        }

        public void Stop() {
            this.AutoPlay = false;
        }

        private void InnerPlay() {
            if (this.AutoPlay)
                Task.Delay(this.Interval)
                    .ContinueWith(t => {
                        if (this.NextRequired != null)
                            this.NextRequired.Invoke(this, new EventArgs());
                        this.InnerPlay();
                    });
        }

        protected override void LayoutChildren(double x, double y, double width, double height) {
            foreach (var c in this.Children) {
                if (c.Parent == null)
                    c.Parent = this;
                c.Layout(new Rectangle(x, y, width, height));
            }
        }
    }

}
