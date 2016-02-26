using Microsoft.AspNet.Builder;

namespace Sogeti.Academy.Infrastructure.Pipeline
{
    public interface IPipelineConfigurator
    {
        void Configure(IApplicationBuilder app);
    }
}
