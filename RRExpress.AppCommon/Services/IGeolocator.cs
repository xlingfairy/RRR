using System;

namespace RRExpress.AppCommon.Services {

    /// <summary>
    /// 位置服务
    /// </summary>
    public interface IGeolocator {
        void GetLocationAsync();

        event EventHandler<LocationGetCallbackEventArgs> LoationGetCallback;

    }
}
