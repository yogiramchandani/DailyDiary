using System.Web.Http;

namespace DailyDiaryApi.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{id}",
                defaults: new { controller = "DailyDiary", id = RouteParameter.Optional }
            );
        }
    }
}