using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RRExpress.Moq.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RRExpress.Service {
    public class Startup {

        public void Configuration(IAppBuilder app) {

            var config = new HttpConfiguration();

            // Route Config
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            // Config Json.Net, 
            // let it can serialize or deserialize 
            // abstrict class.
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings() {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto
            };


            //Use Moq User Authorization
            app.UseMoqAuth();

            app.UseWebApi(config);
            config.EnsureInitialized();
        }

    }
}
