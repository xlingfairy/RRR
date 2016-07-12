﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreAnimation;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using AsNum.XFControls;
using AsNum.XFControls.iOS;

[assembly: ExportRenderer(typeof(CircleBox), typeof(CircleBoxRender))]
namespace AsNum.XFControls.iOS {

    public class CircleBoxRender : VisualElementRenderer<CircleBox> {
        protected override void OnElementChanged(ElementChangedEventArgs<CircleBox> e) {
            base.OnElementChanged(e);
            this.SetLayout();
        }

        private CGColor BgColor;

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            //未命中
            //if (e.PropertyName.Equals(CycleBox.RadiusProperty.PropertyName)) {
            //    var w = this.Element.Radius * 2;
            //    this.Element.WidthRequest = w;
            //    this.Element.HeightRequest = w;
            //}
        }

        private void SetLayout() {
            if (this.Element != null) {
                var w = this.Element.Radius * 2;
                this.Element.WidthRequest = w;
                this.Element.HeightRequest = w;
            }
        }

        protected override void SetBackgroundColor(Color color) {
            //base.SetBackgroundColor(color);
            this.BgColor = this.Element.BackgroundColor.ToCGColor();
            base.SetBackgroundColor(Color.Transparent);
        }

        public override void Draw(CoreGraphics.CGRect rect) {
            //base.Draw(rect);

            var currentContext = UIGraphics.GetCurrentContext();
            var properRect = AdjustForThickness(rect);
            HandleShapeDraw(currentContext, properRect);
        }

        protected RectangleF AdjustForThickness(CGRect rect) {
            var x = rect.X + Element.Padding.Left;
            var y = rect.Y + Element.Padding.Top;
            var width = rect.Width - Element.Padding.HorizontalThickness;
            var height = rect.Height - Element.Padding.VerticalThickness;
            return new RectangleF((float)x, (float)y, (float)width, (float)height);
        }

        protected virtual void HandleShapeDraw(CGContext currentContext, RectangleF rect) {
            var centerX = rect.X + (rect.Width / 2);
            var centerY = rect.Y + (rect.Height / 2);
            var radius = rect.Width / 2;
            var startAngle = 0;
            var endAngle = (float)(Math.PI * 2);

            HandleStandardDraw(currentContext, rect, () => currentContext.AddArc(centerX, centerY, radius, startAngle, endAngle, true));
        }

        /// <summary>
        /// A simple method for handling our drawing of the shape. This method is called differently for each type of shape
        /// </summary>
        /// <param name="currentContext">Current context.</param>
        /// <param name="rect">Rect.</param>
        /// <param name="createPathForShape">Create path for shape.</param>
        protected virtual void HandleStandardDraw(CGContext currentContext, RectangleF rect, Action createPathForShape) {
            currentContext.SetFillColor(this.BgColor);
            createPathForShape();
            currentContext.DrawPath(CGPathDrawingMode.Fill);
        }
    }
}