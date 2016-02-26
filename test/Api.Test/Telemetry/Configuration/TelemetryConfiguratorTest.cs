namespace Sogeti.Academy.Api.Test.Telemetry.Configuration
{
    using System.IO;
    using Api.Telemetry.Configuration;
    using Microsoft.Extensions.Configuration;
    using Xunit;
    using System.Linq;
    using Microsoft.Extensions.Configuration.Json;

    public class TelemetryConfiguratorTest
    {
        private readonly IConfigurationBuilder _configurationBuilder;
        private readonly TelemetryConfigurator _telemetryConfigurator;

        public TelemetryConfiguratorTest()
        {
            _configurationBuilder = ConfigurationBuilderFactory.Create();
            _telemetryConfigurator = new TelemetryConfigurator();
        }

        [Fact]
        public void Configure_ShouldAddTelemetryJson()
        {
            var expectedPath = Path.Combine(ConfigurationBuilderFactory.BasePath, "Telemetry", "Configuration", "telemetry.json");
            Configure();
            var provider = (JsonConfigurationProvider)_configurationBuilder.Providers.First();
            Assert.Equal(expectedPath, provider.Path);
        }

        private void Configure()
        {
            _telemetryConfigurator.Configure(_configurationBuilder);
        }
    }
}
