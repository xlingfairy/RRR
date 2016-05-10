using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Services {
    public interface IWXPay {

        void Pay(string title, decimal fee);

    }
}
