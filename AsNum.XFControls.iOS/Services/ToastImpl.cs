using AsNum.XFControls.iOS.Services;
using AsNum.XFControls.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToasImpl))]
namespace AsNum.XFControls.iOS.Services {
    public class ToasImpl : IToast {

        public void Show(string msg, bool longShow = false) {
            var font = UIFont.SystemFontOfSize(12F);

            var lbl = new UILabel() {
                Text = msg,
                Font = font,
                TextColor = UIColor.White
            };

            //lbl.SizeToFit();

            Toast.Instance.SetContent(lbl);
            Toast.Instance.Show(duration: longShow ? Toast.Durations.Long : Toast.Durations.Short);

            //try {
            //    Device.BeginInvokeOnMainThread(() => {
            //        var toast = Toast.MakeText(Forms.Context, msg, longShow ? ToastLength.Long : ToastLength.Short);
            //        toast.Show();
            //        toast.Dispose();
            //    });
            //} catch (Exception) {

            //}
        }

    }
}
