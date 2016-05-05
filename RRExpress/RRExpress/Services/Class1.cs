using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Services {
    public class LocationGetCallbackEventArgs : EventArgs {

        public double Lat { get; set; }

        public double Lnt { get; set; }

        public string Name { get; set; }

    }
}
