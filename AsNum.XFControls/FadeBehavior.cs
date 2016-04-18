using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {

    //https://adventuresinxamarinforms.com/2015/04/20/animating-the-tabbedview/
    public class FadeBehavior : BindableBehavior<VisualElement> {
        public FadeBehavior() {
            FadeInAnimationLength = 250;
            FadeOutAnimationLength = 350;
        }


        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected",
                typeof(bool),
                typeof(FadeBehavior),
                false,
                BindingMode.Default);

        private static void IsSelectedChanged(BindableObject bindable, bool oldvalue, bool newvalue) {
            FadeBehavior behavior = bindable as FadeBehavior;
            if (behavior == null || behavior.AssociatedObject == null) return;
            behavior.Animate();
        }

        private void Animate() {

            if (IsSelected)
                AssociatedObject.IsVisible = true;

            AssociatedObject.FadeTo(
                IsSelected ? 1 : 0,
                IsSelected ? FadeInAnimationLength : FadeOutAnimationLength,
                Easing.Linear)
                .ContinueWith(x => {
                    if (!IsSelected)
                        AssociatedObject.IsVisible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public bool IsSelected {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public uint FadeInAnimationLength { get; set; }

        public uint FadeOutAnimationLength { get; set; }

        protected override void OnAttachedTo(VisualElement visualElement) {
            base.OnAttachedTo(visualElement);
            visualElement.Opacity = 0;
            visualElement.IsVisible = false;
        }
    }
}
