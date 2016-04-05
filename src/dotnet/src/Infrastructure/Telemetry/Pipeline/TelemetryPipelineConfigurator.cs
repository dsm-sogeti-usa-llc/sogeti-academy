using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Infrastructure.Pipeline;

namespace Sogeti.Academy.Infrastructure.Telemetry.Pipeline
{
    public class TelemetryPipelineConfigurator : IPipelineConfigurator
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseApplicationInsightsExceptionTelemetry();
            app.UseApplicationInsightsRequestTelemetry();
        }
    }
}
