using Microsoft.Azure.Documents;

namespace Sogeti.Academy.Persistence.Storage
{
    public interface IResourceResponse<out T> where T : Resource, new ()
    {
        T Resource { get; }
    }
}
