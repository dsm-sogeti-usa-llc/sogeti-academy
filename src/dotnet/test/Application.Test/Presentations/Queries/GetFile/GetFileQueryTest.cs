using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Queries.GetFile;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Application.Storage;
using Xunit;
using File = Sogeti.Academy.Application.Presentations.Models.File;

namespace Sogeti.Academy.Application.Test.Presentations.Queries.GetFile
{
    public class GetFileQueryTest
    {
        private readonly Mock<IDocumentCollection<Presentation>> _presentationCollectionMock;
        private readonly GetFileQuery _getFileQuery;

        public GetFileQueryTest()
        {
            _presentationCollectionMock = new Mock<IDocumentCollection<Presentation>>();
            var presentationsContextMock = new Mock<IPresentationContext>();
            presentationsContextMock.Setup(s => s.GetCollection<Presentation>()).Returns(_presentationCollectionMock.Object);

            _getFileQuery = new GetFileQuery(presentationsContextMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldGetFile()
        {
            var presentationId = Guid.NewGuid().ToString();

            var expectedBytes = new byte[] {1, 8, 76, 13};
            var presentation = new Presentation
            {
                Id = Guid.NewGuid().ToString(),
                Files = new List<File>
                {
                    new File(),
                    new File
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Job",
                        Type = "Nope",
                        Bytes = expectedBytes
                    },
                    new File()
                }
            };
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(presentationId)).ReturnsAsync(presentation);

            var viewModel = await _getFileQuery.Execute(presentationId, presentation.Files[1].Id);
            Assert.Equal(presentation.Id, viewModel.PresentationId);
            Assert.Equal(presentation.Files[1].Id, viewModel.FileId);
            Assert.Equal(presentation.Files[1].Name, viewModel.Name);
            Assert.Equal(presentation.Files[1].Type, viewModel.Type);
            Assert.Equal(expectedBytes, viewModel.Bytes);
        }
    }
}
