using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Razor;
using System.Linq;

namespace Sogeti.Academy.Presentation.General.ViewLocationExpanders
{
    public class FeatureLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return viewLocations
                .Concat(new[] {"~/Presentation/{1}/Views/{0}.cshtml"});
        }
    }
}
