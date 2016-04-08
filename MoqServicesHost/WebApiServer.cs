using Microsoft.Owin.Hosting;
using RRExpress.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace MoqServicesHost {
    class WebApiServer : ServiceControl {

        private IDisposable _WebApp = null;

        public bool Start(HostControl hostControl) {
            try {
                this._WebApp = WebApp.Start<Startup>("http://localhost:5556");
                return true;
            } catch(Exception) {
                return false;
            }
        }

        public bool Stop(HostControl hostControl) {
            if (this._WebApp != null)
                this._WebApp.Dispose();

            return true;
        }
    }
}
