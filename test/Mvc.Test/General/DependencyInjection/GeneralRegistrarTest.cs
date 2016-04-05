using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Mvc.General.DependencyInjection;
using Sogeti.Academy.Mvc.General.Http;
using Test.Infrastructure;
using Xunit;

namespace Mvc.Test.General.DependencyInjection
{
    public class GeneralRegistrarTest
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private readonly GeneralRegistrar _generalRegistrar;

        public GeneralRegistrarTest()
        {
            _services = new ServiceCollection();
            _configuration = new ConfigurationBuilder().Build();
            _generalRegistrar = new GeneralRegistrar();
        }

        [Fact]
        public void RegisterServices_ShouldRegisterHttpClientFactory()
        {
            _generalRegistrar.RegisterServices(_services, _configuration);
            var descriptor = _services.GetDescriptor<IHttpClientFactory, HttpClientFactory>();
            Assert.NotNull(descriptor);
        }
    }
}
