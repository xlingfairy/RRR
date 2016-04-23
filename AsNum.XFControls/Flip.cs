using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// 源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(Flip),
                null,
                propertyChanged: ItemsSourceChanged);


        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(
                "Orientation",
                typeof(ScrollOrientation),
                typeof(Flip),
                ScrollOrientation.Horizontal);

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
        /// 播放间隔
        /// </summary>
        public static readonly BindableProperty IntervalProperty =
            BindableProperty.Create("Interval",
                typeof(int),
                typeof(Flip),
                2000);

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


        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public ScrollOrientation Orientation {
            get {
                return (ScrollOrientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }

        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)this.GetValue(ItemTemplateProperty);
            }
            set {
                this.SetValue(ItemTemplateProperty, value);
            }
        }



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


        public bool ShowIndicator {
            get {
                return (bool)this.GetValue(ShowIndicatorProperty);
            }
            set {
                this.SetValue(ShowIndicatorProperty, value);
            }
        }

        public int Current {
            get {
                return (int)this.GetValue(CurrentProperty);
            }
            set {
                this.SetValue(CurrentProperty, value);
            }
        }



        public IEnumerable<View> Children {
            get;
            private set;
        } = new List<View>();

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var flip = (Flip)bindable;
            flip.CalcChild();
        }

        private void CalcChild() {
            var children = new List<View>();

            if (this.ItemsSource == null || this.ItemTemplate == null)
                return;

            foreach (var o in this.ItemsSource) {
                var view = (View)this.ItemTemplate.CreateContent();
                view.BindingContext = o;
                children.Add(view);
                view.Parent = this;///Must , 如果不设置 Parent , 如果是 StackLayout , 不会显示
            }

            this.Children = children;
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);

            foreach (var c in this.Children) {
                if (c.Parent == null)
                    c.Parent = this;
                c.Layout(new Rectangle(0, 0, width, height));
            }
        }

        private static void AutoPlayChanged(BindableObject bindable, object oldValue, object newValue) {
            var flip = (Flip)bindable;
            if ((bool)newValue) {
                flip.Play();
            }
            else {
                flip.Stop();
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



        private static void CurrentChanged(BindableObject bindable, object oldValue, object newValue) {
            var flip = (Flip)bindable;
            if (flip.IndexRequired != null) {
                flip.IndexRequired.Invoke(flip, new IndexRequestEventArgs((int)newValue));
            }
        }





        public class IndexRequestEventArgs : EventArgs {

            public int Index { get; }

            public IndexRequestEventArgs(int idx) {
                this.Index = idx;
            }
        }
    }

}
