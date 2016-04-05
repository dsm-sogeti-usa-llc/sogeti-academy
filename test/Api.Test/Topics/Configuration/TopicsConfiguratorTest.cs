namespace Sogeti.Academy.Api.Test.Topics.Configuration
{
    using System.IO;
    using System.Linq;
    using Api.Topics.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;
    using Xunit;

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
            Configure();
            var provider = (JsonConfigurationProvider)_configurationBuilder.Providers.First();
            Assert.Equal(expectedPath, provider.Path);
        }

        private void Configure()
        {
            _topicsConfigurator.Configure(_configurationBuilder);
        }
    }
}
