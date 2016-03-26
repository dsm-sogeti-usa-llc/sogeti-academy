using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Queries.GetDetail;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Application.Storage;
using Xunit;

namespace Application.Test.Presentations.Queries.GetDetail
{
    public class GetDetailQueryTest
    {
        private readonly Mock<IDocumentCollection<Presentation>> _presentationCollectionMock;
        private readonly Mock<IPresentationContext> _presentationsContextMock;
        private readonly GetDetailQuery _getDetailQuery;

        public GetDetailQueryTest()
        {
            _presentationCollectionMock = new Mock<IDocumentCollection<Presentation>>();
            _presentationsContextMock = new Mock<IPresentationContext>();
            _presentationsContextMock.Setup(s => s.GetCollection<Presentation>()).Returns(_presentationCollectionMock.Object);

            _getDetailQuery = new GetDetailQuery(_presentationsContextMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldGetPresentation()
        {
            var id = Guid.NewGuid().ToString();
            var presentation = new Presentation
            {
                Id = Guid.NewGuid().ToString(),
                Description = "Good Stuff",
                Topic = "GS"
            };
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(presentation);

            var viewModel = await _getDetailQuery.Execute(id);
            Assert.Equal(presentation.Id, viewModel.Id);
            Assert.Equal(presentation.Description, viewModel.Description);
            Assert.Equal(presentation.Topic, viewModel.Topic);
        }

        [Fact]
        public async Task Execute_ShouldGetPresentationFiles()
        {
            var id = Guid.NewGuid().ToString();
            var presentation = new Presentation
            {
                Files = new List<File>
                {
                    new File
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Pres",
                        Type = "PPT",
                        Bytes = new byte[] {2, 3, 23, 5}
                    },
                    new File
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "TDD",
                        Type = "zip",
                        Bytes = new byte[] {2, 3, 23, 5, 32, 12}
                    },
                    new File
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "doc",
                        Type = "word",
                        Bytes = new byte[] {2, 3, 23, 5, 2}
                    }
                }
            };
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(presentation);

            var viewModel = await _getDetailQuery.Execute(id);
            AreEqual(presentation.Files[0], viewModel.Files[0]);
            AreEqual(presentation.Files[1], viewModel.Files[1]);
            AreEqual(presentation.Files[2], viewModel.Files[2]);
        }

        [Fact]
        public void Dispose_ShouldDisposeContext()
        {
            _getDetailQuery.Dispose();
            _presentationsContextMock.Verify(s => s.Dispose(), Times.Once());
        }

        private static void AreEqual(File file, FileDetailViewModel viewModel)
        {
            Assert.Equal(file.Id, viewModel.Id);
            Assert.Equal(file.Name, viewModel.Name);
            Assert.Equal(file.Type, viewModel.Type);
            Assert.Equal(file.Bytes.Length, viewModel.Size);
        }
    }
}
