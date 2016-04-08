using System.Web.Mvc;
using System.Web.Routing;

namespace Sogeti.Academy.Mvc.General
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;
            routes.IgnoreRoute("{ resource}.axd/{ *pathInfo}");

            routes.MapMvcAttributeRoutes();
        }
    }
}
