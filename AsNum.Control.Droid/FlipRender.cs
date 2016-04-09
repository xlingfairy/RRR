using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.Support.V4.View;
using AW = Android.Widget;
using AV = Android.Views;
using Xamarin.Forms;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics.Drawables;
using AsNum.XFControls;
using AsNum.XFControls.Droid;

[assembly: ExportRenderer(typeof(Flip), typeof(FlipRender))]
namespace AsNum.XFControls.Droid {
    public class FlipRender : ViewRenderer<Flip, AW.RelativeLayout> {

        private int Count = 0;

        private ViewPager VP = null;
        private LinearLayout PointsContainer = null;
        private int LastPos = 0;

        private static readonly Color DefaultPointColor = Color.Gray;

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);

            this.Count = this.Element.Children.Count();

            var root = new AW.RelativeLayout(this.Context);
            //root.SetBackgroundColor(Color.Green.ToAndroid());
            this.VP = new ViewPager(this.Context);
            this.VP.PageSelected += VP_PageSelected;

            //如果传入的 items 是 IEnumerable 类型的 (未ToList) , 会一直去计算那个 IEnumerable , 可断点到 GetChildrenViews 里, 会一直在那里执行, 从而导致子视图不显示
            var adapter = new FlipViewAdapter(this.VP, this.GetChildrenViews().ToList());
            this.VP.Adapter = adapter;
            this.VP.AddOnPageChangeListener(adapter);
            root.AddView(this.VP, LayoutParams.MatchParent, LayoutParams.MatchParent);

            this.PointsContainer = new LinearLayout(this.Context);
            this.PointsContainer.Orientation = Orientation.Horizontal;

            var lp = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, 20);
            lp.AddRule(LayoutRules.AlignParentBottom);
            lp.AddRule(LayoutRules.CenterHorizontal);
            root.AddView(this.PointsContainer, lp);

            this.SetNativeControl(root);

            this.SetPoints();

            root.Invalidate();
            root.RequestLayout();

            this.Element.NextRequired += Element_NextRequired;
        }

        private void Element_NextRequired(object sender, EventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                //this.VP.CurrentItem++;
                //var pos = this.VP.CurrentItem + 1;
                //this.VP.SetCurrentItem(pos, false);
                ((FlipViewAdapter)this.VP.Adapter).Next();
            });
        }

        private void VP_PageSelected(object sender, ViewPager.PageSelectedEventArgs e) {
            this.SetPointColor(this.LastPos);
            var realPos = e.Position % this.Element.Children.Count();
            this.SetPointColor(realPos, Color.White);
        }

        private IEnumerable<AV.View> GetChildrenViews() {
            foreach (var v in this.Element.Children) {
                //var render = RendererFactory.GetRenderer(v);
                var render = Platform.CreateRenderer(v);
                var c = new AW.FrameLayout(this.Context);
                //c.SetBackgroundColor(Color.Blue.ToAndroid());
                c.AddView(render.ViewGroup, LayoutParams.MatchParent, LayoutParams.MatchParent);
                yield return c;
            }
        }

        private void SetPoints() {
            var lp = new LinearLayout.LayoutParams(10, 10);
            lp.LeftMargin = 5;
            lp.RightMargin = 5;

            var shape = new OvalShape();
            shape.Resize(10, 10);
            var dr = new ShapeDrawable(shape);
            dr.Paint.Color = DefaultPointColor.ToAndroid();

            for (var i = 0; i < this.Count; i++) {
                var v = new AV.View(this.Context);
                //v.SetBackgroundDrawable(dr);
                v.Background = dr;

                this.PointsContainer.AddView(v, lp);
            }
        }

        private void SetPointColor(int idx, Color? color = null) {
            var point = this.PointsContainer.GetChildAt(idx);
            if (point != null) {
                var shape = new OvalShape();
                var dr = new ShapeDrawable(shape);
                dr.Paint.Color = (color ?? DefaultPointColor).ToAndroid();
                //point.SetBackgroundDrawable(dr);
                point.Background = dr;
            }
            this.LastPos = idx;
        }
    }
}