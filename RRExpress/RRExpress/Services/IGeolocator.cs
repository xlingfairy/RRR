using System;

namespace RRExpress.Services {

    /// <summary>
    /// 位置服务
    /// </summary>
    public interface IGeolocator {
        void GetLocationAsync();

        event EventHandler<LocationGetCallbackEventArgs> LoationGetCallback;

    }
}
