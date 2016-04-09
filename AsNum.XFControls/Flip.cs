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

        public event EventHandler NextRequired;

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

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                "ItemTemplate",
                typeof(DataTemplate),
                typeof(Flip),
                null
                );


        public static readonly BindableProperty AutoPlayProperty =
            BindableProperty.Create(
                    "AutoPlay",
                    typeof(bool),
                    typeof(Flip),
                    false,
                    propertyChanged: AutoPlayChanged
                );


        public static readonly BindableProperty IntervalProperty =
            BindableProperty.Create("Interval",
                typeof(int),
                typeof(Flip),
                2000);

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
            } else {
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
    }
}
