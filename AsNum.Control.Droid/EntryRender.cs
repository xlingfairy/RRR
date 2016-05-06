using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(Entry), typeof(EntryRender))]
namespace AsNum.XFControls.Droid {
    public class EntryRender : EntryRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);

            if (this.Element != null) {
                this.Control.SetPadding(0, 20, 0, 20);
                this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}