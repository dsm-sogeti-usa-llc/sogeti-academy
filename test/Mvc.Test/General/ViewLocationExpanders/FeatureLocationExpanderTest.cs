using System.Collections.Generic;
using Sogeti.Academy.Mvc.General.ViewLocationExpanders;
using Xunit;

namespace Mvc.Test.General.ViewLocationExpanders
{
    public class FeatureLocationExpanderTest
    {
        private readonly FeatureLocationExpander _featureLocationExpander;

        public FeatureLocationExpanderTest()
        {
            _featureLocationExpander = new FeatureLocationExpander();
        }

        [Fact]
        public void ExpandViewLocations_ShouldAddFeatureLevelViewsToViewLocations()
        {
            var viewLocations = new List<string>();
            var actual = _featureLocationExpander.ExpandViewLocations(null, viewLocations);

            Assert.Contains("~/{1}/Views/{0}.cshtml", actual);
        }
    }
}
