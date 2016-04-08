using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace Sogeti.Academy.Persistence.Storage
{
    public interface IDocumentClient : IDisposable
    {
        IOrderedQueryable<Database> CreateDatabaseQuery();
        Task<IResourceResponse<Database>> CreateDatabaseAsync(Database database);
        IOrderedQueryable<DocumentCollection> CreateDocumentCollectionQuery(string selfLink);
        Task<IResourceResponse<DocumentCollection>> CreateDocumentCollectionAsync(string selfLink, DocumentCollection collection);
        IOrderedQueryable<T> CreateDocumentQuery<T>(string selfLink);
        Task<IResourceResponse<Document>> CreateDocumentAsync(string selfLink, object item);
        Task<IResourceResponse<Document>> UpsertDocumentAsync(string selfLink, Document document);
        Task<IResourceResponse<Document>> DeleteDocumentAsync(string selfLink);
    }
}