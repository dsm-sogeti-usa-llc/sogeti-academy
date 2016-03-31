using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Infrastructure.Startup;

namespace Sogeti.Academy.Mvc
{
    public class Startup
    {
        private readonly IStartupService _startupService;

        public Startup()
        {
            _startupService = new StartupService(typeof(Startup).Assembly, typeof(StartupService).Assembly);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            _startupService.ConfigureServices(services);
            services.Configure<RazorViewEngineOptions>(_startupService.ConfigureViewsLocations);
        }

        public void Configure(IApplicationBuilder app)
        {
            _startupService.Configure(app);
            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            app.UseStaticFiles();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
