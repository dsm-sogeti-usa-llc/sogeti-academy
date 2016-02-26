using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Infrastructure.DependencyInjection;
using Sogeti.Academy.Infrastructure.Locator;
using Sogeti.Academy.Infrastructure.Pipeline;

namespace Sogeti.Academy.Api
{
    public class Startup
    {
        private static readonly Assembly StartupAssembly = typeof(Startup).Assembly;
        private readonly ILocator<IRegistrar> _registrarLocator;
        private readonly ILocator<IViewLocationExpander> _viewLocationExpanderLocator;
        private readonly ILocator<IPipelineConfigurator> _pipelineConfiguratorLocator;
        private readonly IConfiguration _configuration;

        public Startup()
        {
            _registrarLocator = new Locator<IRegistrar>();
            _viewLocationExpanderLocator = new Locator<IViewLocationExpander>();
            _pipelineConfiguratorLocator = new Locator<IPipelineConfigurator>();

            var builder = new ConfigurationBuilder();
            var configuratorLocator = new Locator<IConfigurator>();
            configuratorLocator.Locate(StartupAssembly)
                .ForEach(c => c.Configure(builder));
            builder.AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("Default", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            services.AddInstance(_configuration);

            services.AddMvc();
            services.Configure<RazorViewEngineOptions>(e => {
                var locatorTypes = _viewLocationExpanderLocator.Locate(StartupAssembly);
                foreach (var locator in locatorTypes)
                    e.ViewLocationExpanders.Add(locator);
            });

            _registrarLocator.Locate(StartupAssembly)
                .ForEach(r => r.RegisterServices(services, _configuration));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("Default");
            _pipelineConfiguratorLocator.Locate(StartupAssembly)
                .ForEach(p => p.Configure(app));

            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
