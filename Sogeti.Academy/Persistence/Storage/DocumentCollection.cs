using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Sogeti.Academy.Application.Storage;
using Sogeti.Academy.Infrastructure.Models;
using Sogeti.Academy.Infrastructure.Storage;

namespace Sogeti.Academy.Persistence.Storage
{
    public class DocumentCollection<T> : IDocumentCollection<T> where T : IModel<string>
    {
        private readonly DocumentClient _client;
        private readonly DocumentAttribute _documentAttribute;

        public DocumentCollection(string endpointUrl, string authKey)
        {
            _client = new DocumentClient(new Uri(endpointUrl), authKey);
            _documentAttribute = typeof(T).GetCustomAttribute<DocumentAttribute>();
        }

        public async Task<string> CreateAsync(T item)
        {
            var collection = await GetOrCreateCollection();
            var response = await _client.CreateDocumentAsync(collection.SelfLink, item);
            return response.Resource.ResourceId;
        }

        public async Task UpdateAsync(T item)
        {
            var collection = await GetOrCreateCollection();
            var document = _client.CreateDocumentQuery<Document>(collection.SelfLink)
                .Where(d => d.Id == item.Id)
                .ToArray()
                .Single();
            UpdateDocument(item, document);
            await _client.UpsertDocumentAsync(collection.SelfLink, document);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var collection = await GetOrCreateCollection();
            return _client.CreateDocumentQuery<T>(collection.SelfLink)
                .Where(d => d.Id == id)
                .ToArray()
                .FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var collection = await GetOrCreateCollection();
            return _client.CreateDocumentQuery<T>(collection.DocumentsLink)
                .ToArray();
        }

        public async Task RemoveAsync(string id)
        {
            var collection = await GetOrCreateCollection();
            var document = _client.CreateDocumentQuery(collection.SelfLink)
                .Where(d => d.ResourceId == id)
                .ToArray()
                .First();
            await _client.DeleteDocumentAsync(document.SelfLink);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private async Task<Database> GetOrCreateDatabase()
        {
            var database = _client.CreateDatabaseQuery()
                .Where(d => d.Id == _documentAttribute.DatabaseId)
                .ToArray()
                .FirstOrDefault();

            if (database != null)
                return database;

            database = new Database { Id = _documentAttribute.DatabaseId };
            return await _client.CreateDatabaseAsync(database);
        }

        private async Task<DocumentCollection> GetOrCreateCollection()
        {
            var database = await GetOrCreateDatabase();
            var collection = _client.CreateDocumentCollectionQuery(database.SelfLink)
                .Where(d => d.Id == _documentAttribute.CollectionId)
                .ToArray()
                .FirstOrDefault();

            if (collection != null)
                return collection;

            collection = new DocumentCollection { Id = _documentAttribute.CollectionId };
            return await _client.CreateDocumentCollectionAsync(database.SelfLink, collection);
        }

        private void UpdateDocument(T item, Document document)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var propetyValue = property.GetGetMethod().Invoke(item, null);
                document.SetPropertyValue(property.Name, propetyValue);
            }
        }
    }
}