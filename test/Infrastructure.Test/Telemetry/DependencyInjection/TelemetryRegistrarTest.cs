namespace Infrastructure.Test.Telemetry.DependencyInjection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Sogeti.Academy.Infrastructure.Telemetry.DependencyInjection;
    using Xunit;

    public class TelemetryRegistrarTest
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;
        private readonly TelemetryRegistrar _telemetryRegistrar;

        public TelemetryRegistrarTest()
        {
            _configuration = new ConfigurationBuilder().Build();
            _services = new ServiceCollection();
            _telemetryRegistrar = new TelemetryRegistrar();
        }

        [Fact]
        public void RegisterServices_ShouldAddApplicationInsightsServices()
        {
            _telemetryRegistrar.RegisterServices(_services, _configuration);
            Assert.Equal(10, _services.Count);
        }
    }
}
