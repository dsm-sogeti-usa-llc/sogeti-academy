using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Moq;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Persistence.Storage;
using Xunit;
using File = Sogeti.Academy.Application.Presentations.Models.File;

namespace Sogeti.Academy.Persistence.Test.Presentations
{
    public class PresentationCollectionTest
    {
        private readonly PresentationCollectionStub _presentationCollection;

        public PresentationCollectionTest()
        {
            _presentationCollection = new PresentationCollectionStub(new Mock<IBlobStorage>());
        }

        [Fact]
        public async Task GetAll_ShouldSetStreamOnPresentationFiles()
        {
            var first = AddPresentationWithFiles();
            var firstStream = SetupBlob(first.Files[0]);

            var second = AddPresentationWithFiles();
            var secondStream = SetupBlob(second.Files[0]);

            var third = AddPresentationWithFiles();
            var thirdStream = SetupBlob(third.Files[0]);

            var presentations = (await _presentationCollection.GetAllAsync()).ToArray();
            Assert.Equal(firstStream.ToArray(), presentations[0].Files[0].Bytes);
            Assert.Equal(secondStream.ToArray(), presentations[1].Files[0].Bytes);
            Assert.Equal(thirdStream.ToArray(), presentations[2].Files[0].Bytes);
        }

        [Fact]
        public async Task GetAll_ShouldKeepPresentationValues()
        {
            var first = AddPresentationWithFiles();

            var presentations = (await _presentationCollection.GetAllAsync()).ToArray();
            Assert.Equal(first.Id, presentations[0].Id);
            Assert.Equal(first.Topic, presentations[0].Topic);
            Assert.Equal(first.Description, presentations[0].Description);
        }

        [Fact]
        public async Task GetAll_ShouldKeepFileValues()
        {
            var first = AddPresentationWithFiles();

            var presentations = (await _presentationCollection.GetAllAsync()).ToArray();
            Assert.Equal(first.Files[0].Id, presentations[0].Files[0].Id);
            Assert.Equal(first.Files[0].Size, presentations[0].Files[0].Size);
            Assert.Equal(first.Files[0].Name, presentations[0].Files[0].Name);
            Assert.Equal(first.Files[0].Type, presentations[0].Files[0].Type);
        }

        [Fact]
        public async Task GetById_ShouldSetFileStreamOnAllFiles()
        {
            var presentation = AddPresentationWithFiles(3);
            var first = SetupBlob(presentation.Files[0]);
            var second = SetupBlob(presentation.Files[1]);
            var third = SetupBlob(presentation.Files[2]);

            var actual = await _presentationCollection.GetByIdAsync(presentation.Id);
            Assert.Same(first, actual.Files[0].Bytes);
            Assert.Same(second, actual.Files[1].Bytes);
            Assert.Same(third, actual.Files[2].Bytes);
        }

        [Fact]
        public async Task Create_ShouldUploadFileStreamsToBlobStorage()
        {
            var presentation = AddPresentationWithFiles(3);
            presentation.Files[0].Bytes = Encoding.UTF8.GetBytes("234");
            presentation.Files[1].Bytes = Encoding.UTF8.GetBytes("234");
            presentation.Files[2].Bytes = Encoding.UTF8.GetBytes("234");

            var id = Guid.NewGuid().ToString();
            var responseMock = new Mock<IResourceResponse<Document>>();
            responseMock.Setup(s => s.Resource).Returns(new Document {Id = id});

            _presentationCollection.ClientMock.Setup(s => s.CreateDocumentAsync(_presentationCollection.Collection.SelfLink, presentation))
                .ReturnsAsync(responseMock.Object);

            var actual = await _presentationCollection.CreateAsync(presentation);
            Assert.Equal(id, actual);
            _presentationCollection.BlogStorageMock.Verify(s => s.UploadBlob(presentation.Files[0].Id, presentation.Files[0].Bytes), Times.Once());
            _presentationCollection.BlogStorageMock.Verify(s => s.UploadBlob(presentation.Files[1].Id, presentation.Files[1].Bytes), Times.Once());
            _presentationCollection.BlogStorageMock.Verify(s => s.UploadBlob(presentation.Files[2].Id, presentation.Files[2].Bytes), Times.Once());
        }

        [Fact]
        public async Task Update_ShouldUploadFileStreamsToBlobStorage()
        {
            var presentation = AddPresentationWithFiles(3);
            presentation.Files[0].Bytes = Encoding.UTF8.GetBytes("234");
            presentation.Files[1].Bytes = Encoding.UTF8.GetBytes("234");
            presentation.Files[2].Bytes = Encoding.UTF8.GetBytes("234");

            var responseMock = new Mock<IResourceResponse<Document>>();
            _presentationCollection.ClientMock.Setup(s => s.UpsertDocumentAsync(_presentationCollection.Collection.SelfLink, _presentationCollection.Documents.First()))
                .ReturnsAsync(responseMock.Object);

            await _presentationCollection.UpdateAsync(presentation);
            _presentationCollection.BlogStorageMock.Verify(s => s.UploadBlob(presentation.Files[0].Id, presentation.Files[0].Bytes), Times.Once());
            _presentationCollection.BlogStorageMock.Verify(s => s.UploadBlob(presentation.Files[1].Id, presentation.Files[1].Bytes), Times.Once());
            _presentationCollection.BlogStorageMock.Verify(s => s.UploadBlob(presentation.Files[2].Id, presentation.Files[2].Bytes), Times.Once());
            _presentationCollection.BlogStorageMock.Verify(s => s.DeleteBlob(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task Update_ShouldSkipUploadingEmptyFileStreams()
        {
            var presentation = AddPresentationWithFiles(3);

            var responseMock = new Mock<IResourceResponse<Document>>();
            _presentationCollection.ClientMock.Setup(s => s.UpsertDocumentAsync(_presentationCollection.Collection.SelfLink, _presentationCollection.Documents.First()))
                .ReturnsAsync(responseMock.Object);

            await _presentationCollection.UpdateAsync(presentation);
            _presentationCollection.BlogStorageMock.Verify(s => s.UploadBlob(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Never());
            _presentationCollection.BlogStorageMock.Verify(s => s.DeleteBlob(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task Update_ShouldDeleteBlobsThatAreMissingFromNewPresentation()
        {
            var existingPresentation = AddPresentationWithFiles(3);

            var newPresentation = new Presentation
            {
                Id = existingPresentation.Id,
                Files = new List<File>()
            };

            _presentationCollection.ClientMock.Setup(s => s.UpsertDocumentAsync(_presentationCollection.Collection.SelfLink, _presentationCollection.Documents.First()))
                .ReturnsAsync(new Mock<IResourceResponse<Document>>().Object);

            await _presentationCollection.UpdateAsync(newPresentation);
            _presentationCollection.BlogStorageMock.Verify(s => s.DeleteBlob(existingPresentation.Files[0].Id), Times.Once());
            _presentationCollection.BlogStorageMock.Verify(s => s.DeleteBlob(existingPresentation.Files[1].Id), Times.Once());
            _presentationCollection.BlogStorageMock.Verify(s => s.DeleteBlob(existingPresentation.Files[2].Id), Times.Once());
        }

        private Presentation AddPresentationWithFiles(int count = 1)
        {
            var presentation = new Presentation
            {
                Topic = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                Files = Enumerable.Range(0, count).Select(CreateFile).ToList()
            };
            _presentationCollection.Presentations.Add(presentation);
            return presentation;
        }

        private static File CreateFile(int size)
        {
            return new File
            {
                Name = $"Name: {size}",
                Size = size,
                Type = $"Type: {size}",
                Id = $"{size}"
            };
        }

        private byte[] SetupBlob(File file)
        {
            var bytes = Encoding.UTF8.GetBytes(file.Id);
            _presentationCollection.BlogStorageMock.Setup(s => s.GetBlob(file.Id))
                .ReturnsAsync(bytes);
            return bytes;
        }
    }
}
