using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Sogeti.Academy.Application.Presentations.Commands.Edit;
using Sogeti.Academy.Application.Presentations.Factories;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Application.Storage;
using Xunit;
using File = Sogeti.Academy.Application.Presentations.Models.File;

namespace Sogeti.Academy.Application.Test.Presentations.Commands.Edit
{
    public class EditPresentationCommandTest
    {
        private readonly Mock<IDocumentCollection<Presentation>> _presentationCollectionMock;
        private readonly Mock<IFileFactory> _fileFactoryMock;
        private readonly EditPresentationCommand _editPresentationCommand;

        public EditPresentationCommandTest()
        {
            _presentationCollectionMock = new Mock<IDocumentCollection<Presentation>>();
            var presentationContextMock = new Mock<IPresentationContext>();
            presentationContextMock.Setup(s => s.GetCollection<Presentation>())
                .Returns(_presentationCollectionMock.Object);

            _fileFactoryMock = new Mock<IFileFactory>();

            _editPresentationCommand = new EditPresentationCommand(presentationContextMock.Object, _fileFactoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldUpdatePresentation()
        {
            var viewModel = new EditPresentationViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Topic = "Job",
                Description = Guid.NewGuid().ToString()
            };
            var presentation = new Presentation();
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(viewModel.Id)).ReturnsAsync(presentation);

            await _editPresentationCommand.Execute(viewModel);
            Assert.Equal(viewModel.Topic, presentation.Topic);
            Assert.Equal(viewModel.Description, presentation.Description);
        }

        [Fact]
        public async Task Execute_ShouldUpdatePresentationInCollection()
        {
            var viewModel = new EditPresentationViewModel();
            var presentation = new Presentation();
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(presentation);

            await _editPresentationCommand.Execute(viewModel);
            _presentationCollectionMock.Verify(s => s.UpdateAsync(presentation), Times.Once());
        }

        [Fact]
        public async Task Execute_ShouldSetAddFile()
        {
            var viewModel = new EditPresentationViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Files = new []
                {
                    new EditFileViewModel
                    {
                        Name = "One",
                        Type = "Doc"
                    }
                }
            };

            var newFile = new File();
            _fileFactoryMock.Setup(s => s.Create(viewModel.Files[0])).Returns(newFile);
            var presentation = new Presentation
            {
                Files = new List<File>()
            };
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(viewModel.Id)).ReturnsAsync(presentation);

            await _editPresentationCommand.Execute(viewModel);
            Assert.Contains(newFile, presentation.Files);
        }

        [Fact]
        public async Task Execute_ShouldUpdateFile()
        {
            var viewModel = new EditPresentationViewModel
            {
                Files = new[]
                {
                    new EditFileViewModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Nuts",
                        Type = "DOC",
                        Bytes = new byte[] {12, 15, 78}
                    }
                }
            };
            var presentation = new Presentation
            {
                Files = new List<File>
                {
                    new File
                    {
                        Id = viewModel.Files[0].Id
                    }
                }
            };
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(viewModel.Id)).ReturnsAsync(presentation);

            await _editPresentationCommand.Execute(viewModel);
            Assert.Equal(viewModel.Files[0].Name, presentation.Files[0].Name);
            Assert.Equal(viewModel.Files[0].Type, presentation.Files[0].Type);
            Assert.Equal(viewModel.Files[0].Bytes, presentation.Files[0].Bytes);
        }

        [Fact]
        public async Task Execute_ShouldNotUpdateFileBytes()
        {
            var viewModel = new EditPresentationViewModel
            {
                Files = new[]
                {
                    new EditFileViewModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Nuts",
                        Type = "DOC"
                    }
                }
            };
            var presentation = new Presentation
            {
                Files = new List<File>
                {
                    new File
                    {
                        Id = viewModel.Files[0].Id,
                        Bytes = new byte[] {5, 3, 7, 5}
                    }
                }
            };
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(viewModel.Id)).ReturnsAsync(presentation);

            await _editPresentationCommand.Execute(viewModel);
            Assert.NotEqual(viewModel.Files[0].Bytes, presentation.Files[0].Bytes);
        }

        [Fact]
        public async Task Execute_ShouldNotUpdateFileSize()
        {
            var viewModel = new EditPresentationViewModel
            {
                Files = new[]
                {
                    new EditFileViewModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Nuts",
                        Type = "DOC"
                    }
                }
            };
            var presentation = new Presentation
            {
                Files = new List<File>
                {
                    new File
                    {
                        Id = viewModel.Files[0].Id,
                        Bytes = new byte[] {5, 3, 7, 5},
                        Size = 534
                    }
                }
            };
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(viewModel.Id)).ReturnsAsync(presentation);

            await _editPresentationCommand.Execute(viewModel);
            Assert.Equal(534, presentation.Files[0].Size);
        }

        [Fact]
        public async Task Execute_ShouldRemoveFile()
        {
            var viewModel = new EditPresentationViewModel
            {
                Id = Guid.NewGuid().ToString()
            };
            var presentation = new Presentation
            {
                Files = new List<File>
                {
                    new File
                    {
                        Id = Guid.NewGuid().ToString()
                    }
                }
            };
            _presentationCollectionMock.Setup(s => s.GetByIdAsync(viewModel.Id)).ReturnsAsync(presentation);

            await _editPresentationCommand.Execute(viewModel);
            Assert.Empty(presentation.Files);
        }
    }
}
