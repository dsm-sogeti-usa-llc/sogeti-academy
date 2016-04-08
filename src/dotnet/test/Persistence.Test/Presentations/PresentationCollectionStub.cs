using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.Documents;
using Moq;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Infrastructure.Storage;
using Sogeti.Academy.Persistence.Presentations.Storage;
using Sogeti.Academy.Persistence.Storage;

namespace Sogeti.Academy.Persistence.Test.Presentations
{
    public class PresentationCollectionStub : PresentationCollection
    {
        public readonly Database Database;

        public IEnumerable<Document> Documents
        {
            get { return Presentations.Select(p => new PresentationDocument(p)); }
        } 
        public readonly DocumentCollection Collection;
        public readonly List<Presentation> Presentations;
        public readonly Mock<IDocumentClient> ClientMock;
        public readonly Mock<IBlobStorage> BlogStorageMock;

        public PresentationCollectionStub(Mock<IBlobStorage> blobStorageMock)
            : base("", "", blobStorageMock.Object)
        {
            var documentAttribute = typeof(Presentation).GetCustomAttribute<DocumentAttribute>();
            Database = new Database
            {
                Id = documentAttribute.DatabaseId
            };
            Collection = new DocumentCollection
            {
                Id = documentAttribute.CollectionId
            };

            ClientMock = new Mock<IDocumentClient>();
            ClientMock.Setup(s => s.CreateDatabaseQuery())
                .Returns(new[] {Database}.AsQueryable().OrderBy(d => d.Id));

            ClientMock.Setup(s => s.CreateDocumentCollectionQuery(Database.SelfLink))
                .Returns(new[] {Collection}.AsQueryable().OrderBy(c => c.Id));

            Presentations = new List<Presentation>();
            ClientMock.Setup(s => s.CreateDocumentQuery<Presentation>(Collection.SelfLink))
                .Returns<string>(link => Presentations.AsQueryable().OrderBy(d => d.Id));

            ClientMock.Setup(s => s.CreateDocumentQuery<Document>(Collection.SelfLink))
                .Returns<string>(link => Documents.AsQueryable().OrderBy(d => d.Id));

            BlogStorageMock = blobStorageMock;
        }

        protected override IDocumentClient CreateClient()
        {
            return ClientMock.Object;
        }
    }
}