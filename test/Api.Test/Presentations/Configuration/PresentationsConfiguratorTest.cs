using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Sogeti.Academy.Api.Presentations.Configuration;
using Xunit;

namespace Sogeti.Academy.Api.Test.Presentations.Configuration
{
    public class PresentationsConfiguratorTest
    {
        private readonly IConfigurationBuilder _configurationBuilder;
        private readonly PresentationsConfigurator _presentationsConfigurator;

        public PresentationsConfiguratorTest()
        {
            _configurationBuilder = ConfigurationBuilderFactory.Create();
            _presentationsConfigurator = new PresentationsConfigurator();
        }

        [Fact]
        public void Configure_ShouldAddPresentationsJson()
        {
            var expectedPath = Path.Combine(ConfigurationBuilderFactory.BasePath, "Presentations", "Configuration", "presentations.json");
            _presentationsConfigurator.Configure(_configurationBuilder);
            var provider = (JsonConfigurationProvider) _configurationBuilder.Providers.First();
            Assert.Equal(expectedPath, provider.Path);
        }
    }
}
