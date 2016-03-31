using System;
using System.Threading.Tasks;
using Moq;
using Sogeti.Academy.Application.Presentations.Commands.Remove;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Application.Storage;
using Xunit;

namespace Application.Test.Presentations.Commands.Remove
{
    public class RemovePresentationCommandTest
    {
        private readonly Mock<IDocumentCollection<Presentation>> _presentationCollectionMock;
        private readonly Mock<IPresentationContext> _presentationContextMock;
        private readonly RemovePresentationCommand _removePresentationCommand;

        public RemovePresentationCommandTest()
        {
            _presentationCollectionMock = new Mock<IDocumentCollection<Presentation>>();
            _presentationContextMock = new Mock<IPresentationContext>();
            _presentationContextMock.Setup(s => s.GetCollection<Presentation>()).Returns(_presentationCollectionMock.Object);

            _removePresentationCommand = new RemovePresentationCommand(_presentationContextMock.Object);
        }

        [Fact]
        public void Dispose_ShouldDisposeContext()
        {
            _removePresentationCommand.Dispose();
            _presentationContextMock.Verify(s => s.Dispose(), Times.Once());
        }

        [Fact]
        public async Task Execute_ShouldRemovePresentation()
        {
            var viewModel = new RemovePresentationViewModel
            {
                Id = Guid.NewGuid().ToString()
            };

            await _removePresentationCommand.Execute(viewModel);
            _presentationCollectionMock.Verify(s => s.RemoveById(viewModel.Id), Times.Once());
        }
    }
}
