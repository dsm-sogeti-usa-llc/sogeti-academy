using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Sogeti.Academy.Infrastructure.Startup;

namespace Sogeti.Academy.Api
{
    public class Startup
    {
        private static readonly Assembly StartupAssembly = typeof(Startup).Assembly;
        private readonly IStartupService _startupService;

        public Startup()
        {
            _startupService = new StartupService(typeof(Startup).Assembly, typeof(StartupService).Assembly);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("Default", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            services.AddMvc()
                .AddJsonOptions(o => o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            _startupService.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("Default");
            _startupService.Configure(app);

            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
