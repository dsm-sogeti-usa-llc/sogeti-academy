using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Sogeti.Academy.Persistence.Storage.Adapters
{
    public class DocumentClientAdapter : IDocumentClient
    {
        private readonly DocumentClient _documentClient;

        public DocumentClientAdapter(Uri uri, string authKey)
        {
            _documentClient = new DocumentClient(uri, authKey);
        }

        public void Dispose()
        {
            _documentClient.Dispose();
        }

        public IOrderedQueryable<Database> CreateDatabaseQuery()
        {
            return _documentClient.CreateDatabaseQuery();
        }

        public async Task<IResourceResponse<Database>> CreateDatabaseAsync(Database database)
        {
            return new ResourceResponseAdapter<Database>(await _documentClient.CreateDatabaseAsync(database));
        }

        public IOrderedQueryable<DocumentCollection> CreateDocumentCollectionQuery(string selfLink)
        {
            return _documentClient.CreateDocumentCollectionQuery(selfLink);
        }

        public async Task<IResourceResponse<DocumentCollection>> CreateDocumentCollectionAsync(string selfLink, DocumentCollection collection)
        {
            return new ResourceResponseAdapter<DocumentCollection>(await _documentClient.CreateDocumentCollectionAsync(selfLink, collection));
        }

        public IOrderedQueryable<T> CreateDocumentQuery<T>(string selfLink)
        {
            return _documentClient.CreateDocumentQuery<T>(selfLink);
        }

        public async Task<IResourceResponse<Document>> CreateDocumentAsync(string selfLink, object item)
        {
            return new ResourceResponseAdapter<Document>(await _documentClient.CreateDocumentAsync(selfLink, item));
        }

        public async Task<IResourceResponse<Document>> UpsertDocumentAsync(string selfLink, Document document)
        {
            return new ResourceResponseAdapter<Document>(await _documentClient.UpsertDocumentAsync(selfLink, document));
        }

        public async Task<IResourceResponse<Document>> DeleteDocumentAsync(string selfLink)
        {
            return new ResourceResponseAdapter<Document>(await _documentClient.DeleteDocumentAsync(selfLink));
        }
    }
}
