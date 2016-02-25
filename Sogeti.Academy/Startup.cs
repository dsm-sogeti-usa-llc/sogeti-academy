using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Cors;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Infrastructure.DependencyInjection;
using Sogeti.Academy.Infrastructure.Locator;
using Sogeti.Academy.Infrastructure.Pipeline;

namespace Sogeti.Academy
{
    public class Startup
    {
        private const string CorsPolicyName = "SogetiAcademyPolicy";
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
            services.AddInstance(_configuration);

            services.AddMvc();
            services.AddCors(builder => builder.AddPolicy(CorsPolicyName,
                policy => {
                    policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                }));
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(CorsPolicyName));
            });
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
            _pipelineConfiguratorLocator.Locate(StartupAssembly)
                .ForEach(p => p.Configure(app));

            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            app.UseCors(CorsPolicyName);
            app.UseStaticFiles();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
