using System.Web.Mvc;

namespace Sogeti.Academy.Mvc.General
{
    public class FeatureViewEngine : RazorViewEngine
    {
        public FeatureViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/{1}/Views/{0}.cshtml"
            };

            PartialViewLocationFormats = new[]
            {
                "~/Shared/Views/{0}.cshtml"
            };
        }
    }
}
