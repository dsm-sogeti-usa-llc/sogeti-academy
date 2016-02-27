using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Sogeti.Academy.Mvc.Telemetry.Configuration;
using Xunit;

namespace Mvc.Test.Telemetry.Configuration
{
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
