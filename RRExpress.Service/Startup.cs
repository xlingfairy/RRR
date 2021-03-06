﻿using Owin;
using RRExpress.Common.Extends;
using RRExpress.Moq.Auth;
using System.Web.Http;

namespace RRExpress.Service {
    public class Startup {

        public void Configuration(IAppBuilder app) {

            var config = new HttpConfiguration();

            //支持直接路由
            config.MapHttpAttributeRoutes();

            // 路由表配置
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            //配置 Json.Net , 使其支持抽象类的序列化及反序列化
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings() {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto
            };

            // 使用 Protobuf
            config.UseProtobuf();

            config.EnableSystemDiagnosticsTracing();

            //TODO 这里使用的是模拟认证，请更换
            app.UseMoqAuth();


            app.UseWebApi(config);



            config.EnsureInitialized();
        }

    }
}
