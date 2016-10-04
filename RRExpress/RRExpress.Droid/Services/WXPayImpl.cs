using RRExpress.AppCommon.Services;
using RRExpress.Droid.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(WXPayImpl))]
namespace RRExpress.Droid.Services {
    public class WXPayImpl : IWXPay {
        public async Task Pay(string title, decimal fee) {
            var wp = new WXPay.WXPay(Forms.Context);
            var pid = await wp.GoPrePayId(title, fee);
            var req = wp.GenPayReq(pid);
            wp.SendPayReq(req);
        }
    }
}