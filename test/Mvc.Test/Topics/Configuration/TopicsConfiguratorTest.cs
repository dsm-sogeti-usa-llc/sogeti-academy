using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Sogeti.Academy.Mvc.Topics.Configuration;
using Xunit;

namespace Mvc.Test.Topics.Configuration
{
    public class TopicsConfiguratorTest
    {
        private readonly IConfigurationBuilder _configurationBuilder;
        private readonly TopicsConfigurator _topicsConfigurator;

        public TopicsConfiguratorTest()
        {
            _configurationBuilder = ConfigurationBuilderFactory.Create();
            _topicsConfigurator = new TopicsConfigurator();
        }

        [Fact]
        public void Configure_ShouldAddTopicsJson()
        {
            var expectedPath = Path.Combine(ConfigurationBuilderFactory.BasePath, "Topics", "Configuration", "topics.json");

            _topicsConfigurator.Configure(_configurationBuilder);
            var provider = (JsonConfigurationProvider)_configurationBuilder.Providers.First();
            Assert.Equal(expectedPath, provider.Path);
        }
    }
}
