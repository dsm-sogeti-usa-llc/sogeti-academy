using Sogeti.Academy.Mvc.General;
using Xunit;

namespace Sogeti.Academy.Mvc.Test.General.ViewLocationExpanders
{
    public class FeatureLocationExpanderTest
    {
        private readonly FeatureViewEngine _featureViewEngine;

        public FeatureLocationExpanderTest()
        {
            _featureViewEngine = new FeatureViewEngine();
        }

        [Fact]
        public void ExpandViewLocations_ShouldAddFeatureLevelViewsToViewLocations()
        {
            Assert.Contains("~/{1}/Views/{0}.cshtml", _featureViewEngine.ViewLocationFormats);
        }
    }
}
