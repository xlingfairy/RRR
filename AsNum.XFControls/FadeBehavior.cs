using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {

    //https://adventuresinxamarinforms.com/2015/04/20/animating-the-tabbedview/
    public class FadeBehavior : BindableBehavior<VisualElement> {
        #region IsSelected
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected",
                typeof(bool),
                typeof(FadeBehavior),
                false,
                BindingMode.Default,
                propertyChanged: IsSelectedChanged);

        public bool IsSelected {
            get {
                return (bool)GetValue(IsSelectedProperty);
            }
            set {
                SetValue(IsSelectedProperty, value);
            }
        }

        private static void IsSelectedChanged(BindableObject bindable, object oldvalue, object newvalue) {
            var behavior = bindable as FadeBehavior;
            if (behavior == null || behavior.AssociatedObject == null)
                return;
            behavior.Animate();
        }
        #endregion

        public uint FadeInAnimationLength { get; set; }

        public uint FadeOutAnimationLength { get; set; }

        public FadeBehavior() {
            FadeInAnimationLength = 250;
            FadeOutAnimationLength = 350;
        }

        protected override void OnAttachedTo(VisualElement visualElement) {
            base.OnAttachedTo(visualElement);

            visualElement.Opacity = 0;
            visualElement.IsVisible = false;
        }

        private void Animate() {

            if (this.IsSelected)
                AssociatedObject.IsVisible = true;

            AssociatedObject.FadeTo(
                this.IsSelected ? 1 : 0,
                this.IsSelected ? FadeInAnimationLength : FadeOutAnimationLength,
                Easing.Linear
                ).ContinueWith(x => {
                    if (!IsSelected)
                        AssociatedObject.IsVisible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
