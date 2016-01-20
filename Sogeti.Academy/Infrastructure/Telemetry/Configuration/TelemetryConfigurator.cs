using System.IO;
using Sogeti.Academy.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Sogeti.Academy.Infrastructure.Telemetry.Configuration
{
    public class TelemetryConfigurator : IConfigurator
    {
        public void Configure(ConfigurationBuilder builder)
        {
            builder.AddJsonFile(Path.Combine("Infrastructure", "Telemetry", "Configuration", "telemetry.json"));
        }
    }
}
