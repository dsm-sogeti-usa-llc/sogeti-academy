using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Persistence.Storage;

namespace Sogeti.Academy.Persistence.Presentations.Storage
{
    public class PresentationContext : DocumentContext, IPresentationContext
    {
        private readonly IPresentationBlobStorage _blobStorage;

        public PresentationContext(IConfiguration configuration, IPresentationBlobStorage blobStorage)
            : base(configuration["Presentations:DocumentDbEndpiontUrl"], configuration["Presentations:DocumentDbAuthKey"])
        {
            _blobStorage = blobStorage;
            Use<PresentationCollection, Presentation>(CreatePresentationCollection);
        }

        private PresentationCollection CreatePresentationCollection(string endpointUrl, string dbAuthKey)
        {
            return new PresentationCollection(endpointUrl, dbAuthKey, _blobStorage);
        } 
    }
}
