using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using System.Linq;
using System.Drawing;
using AsNum.XFControls;
using AsNum.XFControls.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Flip), typeof(FlipViewRender))]
namespace AsNum.XFControls.iOS {
    public class FlipViewRender : ViewRenderer<Flip, FlipView> {

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);

            var fv = new FlipView();
            var items = this.GetChildrenViews().ToList();
            fv.SetItems(items);

            this.SetNativeControl(fv);
            this.Control.SizeToFit();
            this.AddSubview(this.Control.PageControl);

            this.Element.NextRequired += Element_NextRequired;
        }

        private void Element_NextRequired(object sender, EventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                this.Control.Next();
            });
        }

        private IEnumerable<UIView> GetChildrenViews() {
            foreach (var v in this.Element.Children) {
                var render = Platform.CreateRenderer(v);// RendererFactory.GetRenderer(v);
                yield return render.NativeView;
            }
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint) {
            this.Control.UpdateLayout(widthConstraint, heightConstraint);
            return UIViewExtensions.GetSizeRequest(this.NativeView, widthConstraint, heightConstraint, 44.0, 44.0);
        }
    }
}