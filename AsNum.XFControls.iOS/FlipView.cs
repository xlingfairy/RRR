using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace AsNum.XFControls.iOS {
    [Register("FlipView")]
    public class FlipView : UIScrollView {

        public UIPageControl PageControl { get; set; }

        private List<UIView> Views = new List<UIView>();

        public FlipView()
            : base() {
            Initialize();
            this.SetUp();
        }

        public FlipView(RectangleF bounds)
            : base(bounds) {
            Initialize();
            this.SetUp();
        }

        void Initialize() {
            //BackgroundColor = UIColor.Red;
        }

        private void SetUp() {
            //Hide Scroll bar
            this.ShowsHorizontalScrollIndicator = false;
            this.ShowsVerticalScrollIndicator = false;

            this.PagingEnabled = true;

            this.Bounces = false;
            this.BouncesZoom = false;

            this.PageControl = new UIPageControl() {
                CurrentPageIndicatorTintColor = UIColor.White,
                PageIndicatorTintColor = UIColor.Gray
            };
            //this.AddSubview(this.PageControl);
            this.Scrolled += FlipView_Scrolled;
        }

        void FlipView_Scrolled(object sender, EventArgs e) {
            var pageWidth = this.Frame.Size.Width;
            var page = (int)Math.Floor((this.ContentOffset.X - pageWidth / 2) / pageWidth) + 1;
            this.PageControl.CurrentPage = page;
        }

        public void SetItems(List<UIView> items) {
            if (items == null || items.Count == 0) {

            } else {
                for (var i = 0; i < items.Count; i++) {
                    var v = new UIView();
                    var item = items[i];
                    this.Views.Add(item);
                    v.AddSubview(item);
                    this.AddSubview(v);
                }
                this.PageControl.Pages = items.Count;
            }
        }

        public void UpdateLayout(double width, double height) {
            this.Frame = new CGRect(0, 0, width, height);
            this.PageControl.Frame = new CGRect(0, height - 20, width, 20);
            //this.BringSubviewToFront(this.PageControl);
            this.SetNeedsLayout();
        }

        public override void LayoutSubviews() {
            base.LayoutSubviews();

            var w = this.Frame.Size.Width;
            var h = this.Frame.Size.Height;
            for (var i = 0; i < this.Views.Count; i++) {
                var v = this.Subviews[i];

                var rect = new CGRect(w * i, 0, w, h);
                v.Frame = rect;
            }

            this.ContentSize = new CGSize(w * this.Views.Count, h);
        }

        public void Next() {
            var offset = this.ContentOffset;
            offset.X += this.Frame.Size.Width;
            if (offset.X >= this.ContentSize.Width)
                offset.X = 0;
            this.SetContentOffset(offset, true);
        }

        public void Goto(int idx) {
            var offset = this.ContentOffset;
            offset.X = this.Frame.Size.Width * idx;
            if (offset.X >= this.ContentSize.Width)
                offset.X = 0;
            this.SetContentOffset(offset, true);
        }
    }
}