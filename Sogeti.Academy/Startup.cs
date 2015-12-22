using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance(_configuration);

            services.AddMvc();
            services.Configure<RazorViewEngineOptions>(e => {
                var locatorTypes = _viewLocationExpanderLocator.GetAll(StartupAssembly);
                foreach (var locator in locatorTypes)
                    e.ViewLocationExpanders.Add(locator);
            });

            _registrarLocator.GetRegistrars(StartupAssembly)
                .ForEach(r => r.RegisterServices(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            app.UseStaticFiles();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
