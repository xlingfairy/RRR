using Microsoft.Owin.Hosting;
using RRExpress.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace MoqServicesHost {
    class WebApiServer : ServiceControl {


        private IDisposable _WebApp = null;

        private int Port;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port">使用端口</param>
        /// <param name="url">基地址</param>
        public WebApiServer(int port = 80) {
            this.Port = port;
        }

        public bool Start(HostControl hostControl) {
            try {
                var opt = new StartOptions() {
                    Port = this.Port
                };
                opt.Urls.Add("http://localhost");
                opt.Urls.Add("http://127.0.0.1");

                var ips = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (var ip in ips) {
                    if (ip.AddressFamily == AddressFamily.InterNetwork) {
                        var str = $"http://{ip.MapToIPv4().ToString()}";
                        opt.Urls.Add(str);
                    }
                }

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
