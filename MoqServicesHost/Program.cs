using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace MoqServicesHost {
    class Program {
        static void Main(string[] args) {

            HostFactory.Run(x => {
                x.SetDescription("RRExpress WebApi MOQ Service");
                x.SetDisplayName("RRExpress WebApi");
                x.SetInstanceName("RRExpress.WebApi");
                x.SetServiceName("RRExpress.WebApi");

                x.Service(s => {
                    var server = new WebApiServer();
                    return server;
                });
            });
        }
    }
}
