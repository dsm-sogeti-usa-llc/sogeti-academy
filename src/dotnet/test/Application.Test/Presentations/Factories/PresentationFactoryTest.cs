using System;
using Moq;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Factories;
using Sogeti.Academy.Application.Presentations.Models;
using Xunit;

namespace Sogeti.Academy.Application.Test.Presentations.Factories
{
    public class PresentationFactoryTest
    {
        private readonly PresentationFactory _presentationFactory;
        private readonly Mock<IFileFactory> _fileFactoryMock;

        public PresentationFactoryTest()
        {
            _fileFactoryMock = new Mock<IFileFactory>();
            _presentationFactory = new PresentationFactory(_fileFactoryMock.Object);
        }

        [Fact]
        public void Create_ShouldSetTopic()
        {
            var viewModel = new AddPresentationViewModel {Topic = "Jackpot"};
            var presentation = _presentationFactory.Create(viewModel);
            Assert.Equal(viewModel.Topic, presentation.Topic);
        }

        [Fact]
        public void Create_ShouldSetDescription()
        {
            var viewModel = new AddPresentationViewModel {Description = "Slots"};
            var presentation = _presentationFactory.Create(viewModel);
            Assert.Equal(viewModel.Description, presentation.Description);
        }

        [Fact]
        public void Create_ShouldSetId()
        {
            var viewModel = new AddPresentationViewModel();
            var presentation = _presentationFactory.Create(viewModel);
            Assert.NotEqual(Guid.Empty, Guid.Parse(presentation.Id));
        }

        [Fact]
        public void Create_ShouldCreateFiles()
        {
            var viewModel = new AddPresentationViewModel
            {
                Files = new[] {new AddFileViewModel(), new AddFileViewModel(), new AddFileViewModel()}
            };
            var firstFile = SetupFile(viewModel.Files[0]);
            var secondFile = SetupFile(viewModel.Files[1]);
            var thirdFile = SetupFile(viewModel.Files[2]);

            var presentation = _presentationFactory.Create(viewModel);
            Assert.Contains(firstFile, presentation.Files);
            Assert.Contains(secondFile, presentation.Files);
            Assert.Contains(thirdFile, presentation.Files);
        }

        [Fact]
        public void Create_ShouldSetEmptyFiles()
        {
            var viewModel = new AddPresentationViewModel();

            var presentation = _presentationFactory.Create(viewModel);
            Assert.Empty(presentation.Files);
        }

        private File SetupFile(AddFileViewModel viewModel)
        {
            var expected = new File();
            _fileFactoryMock.Setup(s => s.Create(viewModel)).Returns(expected);
            return expected;
        }
    }
}
