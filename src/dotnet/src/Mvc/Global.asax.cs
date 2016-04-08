using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sogeti.Academy.Mvc.General;

namespace Sogeti.Academy.Mvc
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewEngine());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}