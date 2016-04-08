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

        private int Port;
        private string Url;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port">使用端口</param>
        /// <param name="url">基地址</param>
        public WebApiServer(string url = "http://localhost", int port = 80) {
            this.Port = port;
            this.Url = url;
        }

        public bool Start(HostControl hostControl) {
            try {
                var opt = new StartOptions(this.Url) {
                    Port = this.Port
                };
                //this._WebApp = WebApp.Start<Startup>("http://localhost:5556");
                this._WebApp = WebApp.Start<Startup>(opt);
                return true;
            }
            catch (Exception) {
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
