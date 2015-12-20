using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Presentation.General.Http;

namespace Sogeti.Academy.Infrastructure.DependencyInjection
{
    public class GeneralRegistrar : IRegistrar
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IHttpClientFactory, HttpClientFactory>();
        }
    }
}