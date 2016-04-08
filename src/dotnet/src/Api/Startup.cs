using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Sogeti.Academy.Api;

[assembly:OwinStartup(typeof(Startup))]
namespace Sogeti.Academy.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(WebApiConfiguration.Create());
        }
    }
}
