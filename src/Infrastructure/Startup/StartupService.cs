using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Infrastructure.DependencyInjection;
using Sogeti.Academy.Infrastructure.Locator;
using Sogeti.Academy.Infrastructure.Pipeline;

namespace Sogeti.Academy.Infrastructure.Startup
{
    public interface IStartupService
    {
        IConfiguration Configuration { get; }
        void ConfigureServices(IServiceCollection services);
        void ConfigureViewsLocations(RazorViewEngineOptions options);
        void Configure(IApplicationBuilder app);
    }

    public class StartupService : IStartupService
    {
        private readonly Assembly[] _assemblies;
        private readonly ILocator<IConfigurator> _configuratorLocator;
        private readonly ILocator<IRegistrar> _registrarLocator;
        private readonly ILocator<IPipelineConfigurator> _pipelineConfiguratorLocator;
        private readonly ILocator<IViewLocationExpander> _viewLocationExpanderLocator;
        private IConfiguration _configuration;

        public IConfiguration Configuration => _configuration ?? (_configuration = BuildConfiguration());

        public StartupService(params Assembly[] assemblies)
            : this(new Locator<IConfigurator>(), new Locator<IRegistrar>(), new Locator<IPipelineConfigurator>(), new Locator<IViewLocationExpander>(), assemblies)
        {
            
        }

        public StartupService(ILocator<IConfigurator> configuratorLocator,
            ILocator<IRegistrar> registrarLocator,
            ILocator<IPipelineConfigurator> pipelineConfiguratorLocator,
            ILocator<IViewLocationExpander> viewLocationExpanderLocator,
            params Assembly[] assemblies)
        {
            _configuratorLocator = configuratorLocator;
            _registrarLocator = registrarLocator;
            _pipelineConfiguratorLocator = pipelineConfiguratorLocator;
            _viewLocationExpanderLocator = viewLocationExpanderLocator;
            _assemblies = assemblies;
            
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance(Configuration);
            Locate(_registrarLocator).ForEach(c => c.RegisterServices(services, Configuration));
        }

        public void ConfigureViewsLocations(RazorViewEngineOptions options)
        {
            Locate(_viewLocationExpanderLocator)
                .ForEach(v => options.ViewLocationExpanders.Add(v));
        }

        public void Configure(IApplicationBuilder app)
        {
            Locate(_pipelineConfiguratorLocator).ForEach(p => p.Configure(app));
        }

        private IConfiguration BuildConfiguration()
        {
            var configurators = Locate(_configuratorLocator);
            var builder = new ConfigurationBuilder();
            configurators.ForEach(c => c.Configure(builder));
            builder.AddEnvironmentVariables();
            return builder.Build();
        }

        private List<T> Locate<T>(ILocator<T> locator)
        {
            return _assemblies.SelectMany(locator.Locate)
                .ToList();
        }
    }
}
