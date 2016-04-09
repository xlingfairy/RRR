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
using Android.Support.V4.View;

namespace AsNum.XFControls.Droid {
    public class FlipViewAdapter : PagerAdapter, ViewPager.IOnPageChangeListener {

        private List<View> Items;

        public override int Count {
            get {
                return this.Items.Count() * 2;
            }
        }

        private ViewPager ViewPager = null;

        public FlipViewAdapter(ViewPager vp, List<View> items) {
            if (items == null)
                throw new ArgumentNullException("items");
            if (items.Count() == 0)
                throw new ArgumentException("items is empty", "items");

            this.Items = items;
            this.ViewPager = vp;
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue) {
            return view.Equals(objectValue);
        }

        public override int GetItemPosition(Java.Lang.Object objectValue) {
            return PositionNone;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position) {
            position %= this.Items.Count();

            var item = this.Items.ElementAt(position);

            if (item.Parent != null) {
                var p = item.Parent as ViewGroup;
                if (p != null)
                    p.RemoveView(item);
            }

            container.AddView(item, 200, 200);
            return item;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue) {
            //var vp = (ViewPager)container;
            //var chd = container.GetChildAt(position);
            //不能 Dispose, 如果释放了,会在 FinishUpdate 的时候, 报错
            //chd.Dispose();
            //container.RemoveView((View)chd);
        }


        private bool IsManual = false;
        /// <summary>
        /// only occured by gesture
        /// </summary>
        /// <param name="state"></param>
        public void OnPageScrollStateChanged(int state) {
            this.IsManual = true;
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels) {
            //Do nothing
        }

        public void OnPageSelected(int pos) {
            if (IsManual) {
                if (pos == 0) {
                    pos = this.Items.Count;/////////////
                    this.ViewPager.SetCurrentItem(pos, false);
                } else if (pos == this.Count - 1) {
                    pos = this.Items.Count - 1;///////////////
                    this.ViewPager.SetCurrentItem(pos, false);
                }
            }
        }

        public void Next() {
            this.IsManual = false;
            var pos = (this.ViewPager.CurrentItem + 1) % this.Items.Count;
            this.ViewPager.SetCurrentItem(pos, false);
        }
    }
}