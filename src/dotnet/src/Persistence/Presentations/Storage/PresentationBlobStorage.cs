using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Persistence.Storage;

namespace Sogeti.Academy.Persistence.Presentations.Storage
{
    public interface IPresentationBlobStorage : IBlobStorage
    {
    }

    public class PresentationBlobStorage : BlobStorage, IPresentationBlobStorage
    {
        public PresentationBlobStorage()
            : this(new Configuration())
        {
            
        }

        public PresentationBlobStorage(IConfiguration configuration)
            : base(configuration["Presentations:BlobStorageConnectionString"], configuration["Presentations:BlobStorageContainer"])
        {
            
        }
    }
}
