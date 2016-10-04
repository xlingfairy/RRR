using AsNum.XFControls.iOS.Services;
using AsNum.XFControls.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToasImpl))]
namespace AsNum.XFControls.iOS.Services {
    public class ToasImpl : IToast {

        public void Show(string msg, bool longShow = false) {
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
