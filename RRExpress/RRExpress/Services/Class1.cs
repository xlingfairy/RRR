using System;

namespace RRExpress.Services {
    public class LocationGetCallbackEventArgs : EventArgs {

        public double Lat { get; set; }

        public double Lnt { get; set; }

        public string Name { get; set; }

    }
}
