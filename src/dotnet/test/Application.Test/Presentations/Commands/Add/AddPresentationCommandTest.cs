using System;
using System.Threading.Tasks;
using Moq;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Factories;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Application.Storage;
using Xunit;

namespace Sogeti.Academy.Application.Test.Presentations.Commands.Add
{
    public class AddPresentationCommandTest
    {
        private readonly Mock<IDocumentCollection<Presentation>> _presentationCollectionMock;
        private readonly Mock<IPresentationFactory> _presentationFactoryMock;
        private readonly AddPresentationCommand _addPresentationCommand;

        public AddPresentationCommandTest()
        {
            _presentationCollectionMock = new Mock<IDocumentCollection<Presentation>>();
            var presentationContext = new Mock<IPresentationContext>();
            presentationContext.Setup(s => s.GetCollection<Presentation>()).Returns(_presentationCollectionMock.Object);
            _presentationFactoryMock = new Mock<IPresentationFactory>();

            _addPresentationCommand = new AddPresentationCommand(presentationContext.Object, _presentationFactoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCreatePresentation()
        {
            var viewModel = new AddPresentationViewModel();
            var presentation = new Presentation();
            _presentationFactoryMock.Setup(s => s.Create(viewModel)).Returns(presentation);

            var expectedId = Guid.NewGuid().ToString();
            _presentationCollectionMock.Setup(s => s.CreateAsync(presentation)).ReturnsAsync(expectedId);
            var id = await _addPresentationCommand.Execute(viewModel);
            Assert.Equal(expectedId, id);
        }
    }
}
