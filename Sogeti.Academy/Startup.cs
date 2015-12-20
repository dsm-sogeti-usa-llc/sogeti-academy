using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Infrastructure.DependencyInjection;
using Sogeti.Academy.Presentation.General.ViewLocationExpanders;

namespace Sogeti.Academy
{
    public class Startup
    {
		private static readonly Assembly StartupAssembly = typeof(Startup).Assembly;
        private readonly IRegistrarLocator _registrarLocator;
        private readonly IViewLocationExpanderLocator _viewLocationExpanderLocator;
        private readonly IConfiguration _configuration;

        public Startup()
        {
            _registrarLocator = new RegistrarLocator();
            _viewLocationExpanderLocator = new ViewLocationExpanderLocator();

            var builder = new ConfigurationBuilder();
            var configuratorLocator = new ConfiguratorLocator();
            configuratorLocator.GetConfigurators(StartupAssembly)
                .ForEach(c => c.Configure(builder));
            _configuration = builder.Build();	
		}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance(_configuration);

            services.AddMvc();
            services.Configure<RazorViewEngineOptions>(e => {
                var locatorTypes = _viewLocationExpanderLocator.GetAll(StartupAssembly);
                foreach(var locator in locatorTypes)
                    e.ViewLocationExpanders.Add(locator); 
            });
            
            _registrarLocator.GetRegistrars(StartupAssembly)
                .ForEach(r => r.RegisterServices(services));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}