using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace AsNum.XFControls.iOS {
    public class Toast : NSObject {

        private static Lazy<Toast> _Instance = new Lazy<Toast>(() => new Toast());


        public static Toast Instance {
            get {
                return _Instance.Value;
            }
        }


        private Toast() {

        }


        private Lazy<UIView> Container = new Lazy<UIView>(() => {
            var view = new UIView();
            view.Layer.CornerRadius = 10f;
            view.Layer.BackgroundColor = new CoreGraphics.CGColor(0, 0, 0, 0.75f);
            view.AutoresizingMask = UIViewAutoresizing.All;
			view.ContentMode = UIViewContentMode.Center;
			view.AutosizesSubviews = true;

            return view;
        });

        private UIView SubView = null;

        public void SetContent(UIView view) {
            if (this.SubView != null) {
                this.SubView.RemoveFromSuperview();
            }
			view.SizeToFit();
            this.SubView = view;
            this.Container.Value.AddSubview(view);

			var window = UIApplication.SharedApplication.KeyWindow;
			this.Container.Value.Frame = new CoreGraphics.CGRect(0, 0, view.Frame.Width + 10, view.Frame.Height + 10);
			this.Container.Value.Center = new CoreGraphics.CGPoint(window.Center.X, window.Center.Y);

			var x = this.Container.Value.Frame.Width / 2;
			var y = this.Container.Value.Frame.Height / 2;

			this.SubView.Center = new CoreGraphics.CGPoint(x, y);
			this.Container.Value.SizeToFit();
        }

        public void Show(Positions pos = Positions.Bottom, Durations duration = Durations.Short) {
            this.Dismiss();

            var window = UIApplication.SharedApplication.KeyWindow;
            window.AddSubview(this.Container.Value);

            var ms = duration == Durations.Long ? 3000 : 1000;
            Task.Delay(ms)
                .ContinueWith(t =>
                    this.Dismiss()
                );
        }

        public void Dismiss() {
            this.Container.Value.RemoveFromSuperview();
        }


        #region dispose
        private bool IsDisposed = false;

        protected override void Dispose(bool disposing) {
            if (disposing && !this.IsDisposed) {
                this.IsDisposed = true;

                if (this.Container != null && this.Container.IsValueCreated) {
                    this.Container.Value.Dispose();
                    this.Container = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        public enum Positions {
            Top,
            Center,
            Bottom
        }

        public enum Durations {
            Long,
            Short
        }
    }
}
