using System;
using System.Threading.Tasks;
using Moq;
using Sogeti.Academy.Application.Presentations.Commands.Remove;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Application.Storage;
using Xunit;

namespace Sogeti.Academy.Application.Test.Presentations.Commands.Remove
{
    public class RemovePresentationCommandTest
    {
        private readonly Mock<IDocumentCollection<Presentation>> _presentationCollectionMock;
        private readonly RemovePresentationCommand _removePresentationCommand;

        public RemovePresentationCommandTest()
        {
            _presentationCollectionMock = new Mock<IDocumentCollection<Presentation>>();
            var presentationContextMock = new Mock<IPresentationContext>();
            presentationContextMock.Setup(s => s.GetCollection<Presentation>()).Returns(_presentationCollectionMock.Object);

            _removePresentationCommand = new RemovePresentationCommand(presentationContextMock.Object);
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
