using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using Sogeti.Academy.Application.Storage;
using Sogeti.Academy.Infrastructure.Models;
using Sogeti.Academy.Infrastructure.Storage;
using Sogeti.Academy.Persistence.Storage.Adapters;

namespace Sogeti.Academy.Persistence.Storage
{
    public class DocumentCollection<T> : IDocumentCollection<T> where T : IModel<string>
    {
        private readonly string _endpointUrl;
        private readonly string _authKey;
        private readonly DocumentAttribute _documentAttribute;

        public DocumentCollection(string endpointUrl, string authKey)
        {
            _endpointUrl = endpointUrl;
            _authKey = authKey;
            _documentAttribute = typeof(T).GetCustomAttribute<DocumentAttribute>();
        }

        public virtual async Task<string> CreateAsync(T item)
        {
            using (var client = CreateClient())
            {
                var collection = await GetOrCreateCollection();
                var response = await client.CreateDocumentAsync(collection.SelfLink, item);
                return response.Resource.Id;
            }
        }

        public virtual async Task UpdateAsync(T item)
        {
            using (var client = CreateClient())
            {
                var collection = await GetOrCreateCollection();
                var document = await GetDocumentById(item.Id);
                UpdateDocument(item, document);
                await client.UpsertDocumentAsync(collection.SelfLink, document);
            }
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            var doc = await GetDocumentById(id);
            return JsonConvert.DeserializeObject<T>(doc.ToString());
        }

        public virtual async Task RemoveById(string id)
        {
            using (var client = CreateClient())
            {
                var document = await GetDocumentById(id);
                await client.DeleteDocumentAsync(document.SelfLink);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var client = CreateClient())
            {
                var collection = await GetOrCreateCollection();
                return client.CreateDocumentQuery<T>(collection.SelfLink)
                    .ToArray();
            }
        }

        protected virtual IDocumentClient CreateClient()
        {
            return new DocumentClientAdapter(new Uri(_endpointUrl), _authKey);
        }

        private async Task<Database> GetOrCreateDatabase()
        {
            using (var client = CreateClient())
            {
                var database = client.CreateDatabaseQuery()
                .Where(d => d.Id == _documentAttribute.DatabaseId)
                .AsEnumerable()
                .FirstOrDefault();

                if (database != null)
                    return database;

                database = new Database { Id = _documentAttribute.DatabaseId };
                var response = await client.CreateDatabaseAsync(database);
                return response.Resource;
            }
        }

        private async Task<DocumentCollection> GetOrCreateCollection()
        {
            using (var client = CreateClient())
            {
                var database = await GetOrCreateDatabase();
                var collection = client.CreateDocumentCollectionQuery(database.SelfLink)
                    .Where(d => d.Id == _documentAttribute.CollectionId)
                    .AsEnumerable()
                    .FirstOrDefault();

                if (collection != null)
                    return collection;

                collection = new DocumentCollection { Id = _documentAttribute.CollectionId };
                var response = await client.CreateDocumentCollectionAsync(database.SelfLink, collection);
                return response.Resource;
            }
        }

        private async Task<Document> GetDocumentById(string id)
        {
            using (var client = CreateClient())
            {
                var collection = await GetOrCreateCollection();
                return client.CreateDocumentQuery<Document>(collection.SelfLink)
                    .Where(d => d.Id == id)
                    .AsEnumerable()
                    .FirstOrDefault();
            }
        }

        private static void UpdateDocument(T item, Document document)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var propetyValue = property.GetGetMethod().Invoke(item, null);
                document.SetPropertyValue(property.Name, propetyValue);
            }
        }
    }
}