using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Sogeti.Academy.Api
{
    public class WebApiConfiguration : HttpConfiguration
    {
        public WebApiConfiguration()
        {
            this.MapHttpAttributeRoutes();
            this.EnableSystemDiagnosticsTracing();
            Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        public static HttpConfiguration Create()
        {
            return new WebApiConfiguration();
        }
    }
}
