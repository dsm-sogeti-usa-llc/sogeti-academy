using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Infrastructure.DependencyInjection;
using Sogeti.Academy.Mvc.General.Http;

namespace Sogeti.Academy.Mvc.General.DependencyInjection
{
    public class GeneralRegistrar : IRegistrar
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IHttpClientFactory, HttpClientFactory>();
        }
    }
}