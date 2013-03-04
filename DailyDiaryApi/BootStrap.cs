using System;
using System.Web.Http;
using System.Web.Routing;
using DailyDiaryApi.App_Start;
using Newtonsoft.Json.Serialization;

namespace DailyDiaryApi
{
    public class BootStrap
    {
        public void Configure(HttpConfiguration config)
        {
            RouteConfig.RegisterRoutes(config.Routes);

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                new CamelCasePropertyNamesContractResolver();
        }
    }
}