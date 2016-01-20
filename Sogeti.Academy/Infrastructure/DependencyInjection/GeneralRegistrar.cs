using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Presentation.General.Http;

namespace Sogeti.Academy.Infrastructure.DependencyInjection
{
    public class GeneralRegistrar : IRegistrar
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IHttpClientFactory, HttpClientFactory>();
        }
    }
}