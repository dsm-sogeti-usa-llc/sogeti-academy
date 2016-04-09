using System.Net.Http;
using System.Threading.Tasks;
using Sogeti.Academy.Api.General.Http;

namespace Sogeti.Academy.Api.Presentations.Factories
{
    public class MultipartFormDataProviderFactory
    {
        private readonly IServer _server;

        public MultipartFormDataProviderFactory()
            : this(new Server())
        {
            
        }

        public MultipartFormDataProviderFactory(IServer server)
        {
            _server = server;
        }

        public async Task<MultipartFormDataStreamProvider> GetFormDataProvider(HttpRequestMessage request)
        {
            var root = _server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            await request.Content.ReadAsMultipartAsync(provider);
            return provider;
        }
    }
}