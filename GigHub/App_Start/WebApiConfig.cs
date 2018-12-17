using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GigHub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Configuring Camel-Case Format
            var setting = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            setting.Formatting = Formatting.Indented;
            setting.ContractResolver  = new CamelCasePropertyNamesContractResolver();


			config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
