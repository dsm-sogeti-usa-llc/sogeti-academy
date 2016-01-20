using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Infrastructure.DependencyInjection;

namespace Sogeti.Academy.Infrastructure.Telemetry.DependencyInjection
{
    public class TelemetryRegistrar : IRegistrar
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);
        }
    }
}
