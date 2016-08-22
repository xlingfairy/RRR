using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    [ContentProperty("Children")]
    public class Flip : View {

        /// <summary>
        /// 下一侦
        /// </summary>
        public event EventHandler NextRequired;

        /// <summary>
        /// 请求指定侦
        /// </summary>
        public event EventHandler<IndexRequestEventArgs> IndexRequired;


        #region ItemsSource
        /// <summary>
        /// 源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(Flip),
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
            var flip = (Flip)bindable;
            //flip.CalcChild();
            flip.WrapItemsSource();
        }
        #endregion

        #region Orientation
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

        #region ItemTemplate
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

        #region AutoPlay
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

        #region Interval
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
            if (flip.IndexRequired != null && !oldValue.Equals(newValue)) {
                flip.IndexRequired.Invoke(flip, new IndexRequestEventArgs((int)newValue));
            }
        }
        #endregion


        public ObservableCollection<View> Children {
            get;
        } = new ObservableCollection<View>();

        //public ReadOnlyObservableCollection<View> Children {
        //    get;
        //}

        public Flip() {
            //this.Children = new ReadOnlyObservableCollection<View>(this.InternalChildren);
        }

        #region 数据源变动事件
        /// <summary>
        /// 订阅数据源变化通知
        /// </summary>
        private void WrapItemsSource() {
            new NotifyCollectionWrapper(this.ItemsSource,
                            add: (datas, idx) => this.Add(datas, idx),
                            remove: (datas, idx) => this.Remove(datas, idx),
                            reset: () => this.Reset(),
                            finished: () => { });
        }


        private void Add(IList datas, int idx) {
            var c = this.Children.Count();

            foreach (var d in datas) {
                var i = idx++;
                var v = this.GetChild(d);
                if (i < c) {
                    this.Children.Insert(i, v);
                } else {
                    this.Children.Add(v);
                }
            }
        }

        private void Remove(IList datas, int idx) {
            var headers = this.Children.Skip(idx).Take(datas.Count);

            for (var i = idx; i < datas.Count; i++) {
                this.Children.RemoveAt(i);
            }
        }

        private void Reset() {
            this.Children.Clear();

            if (this.ItemsSource != null) {
                var idx = 0;
                foreach (var d in this.ItemsSource) {
                    var i = idx++;
                    var v = this.GetChild(d);
                    this.Children.Add(v);
                }
            }
        }
        #endregion

        //private void CalcChild() {
        //    var children = new List<View>();

        //    if (this.ItemsSource == null || this.ItemTemplate == null)
        //        return;

        //    foreach (var o in this.ItemsSource) {
        //        var view = this.GetChild(o);
        //        children.Add(view);
        //    }

        //    this.Children = children;
        //}

        private View GetChild(object data) {
            View view = null;
            if (this.ItemTemplate != null) {
                view = (View)this.ItemTemplate.CreateContent();
                view.BindingContext = data;
                view.Parent = this;
            } else {
                view = new Label() { Text = "Not Set ItemTemplate" };
            }
            return view;
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





        public class IndexRequestEventArgs : EventArgs {

            public int Index { get; }

            public IndexRequestEventArgs(int idx) {
                this.Index = idx;
            }
        }
    }

}
