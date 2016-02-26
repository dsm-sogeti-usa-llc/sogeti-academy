using System.IO;
using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Infrastructure.Configuration;

namespace Sogeti.Academy.Mvc.Telemetry.Configuration
{
    public class TelemetryConfigurator : IConfigurator
    {
        public void Configure(ConfigurationBuilder builder)
        {
            builder.AddJsonFile(Path.Combine("Telemetry", "Configuration", "telemetry.json"));
        }
    }
}
