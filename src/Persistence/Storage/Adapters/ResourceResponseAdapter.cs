using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Sogeti.Academy.Persistence.Storage.Adapters
{
    public class ResourceResponseAdapter<T> : IResourceResponse<T> where T : Resource, new()
    {
        private readonly ResourceResponse<T> _response;

        public T Resource => _response.Resource;

        public ResourceResponseAdapter(ResourceResponse<T> response)
        {
            _response = response;
        }
    }
}
