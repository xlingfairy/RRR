using System.Threading.Tasks;

namespace RRExpress.AppCommon.Services {
    public interface IWXPay {

        Task Pay(string title, decimal fee);

    }
}
