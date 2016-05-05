using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Services {
    public interface IGeolocator {
        void GetLocationAsync();

        event EventHandler<LocationGetCallbackEventArgs> LoationGetCallback;

    }
}
