using System;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Factories;
using Xunit;

namespace Application.Test.Presentations.Factories
{
    public class FileFactoryTest
    {
        private readonly FileFactory _fileFactory;

        public FileFactoryTest()
        {
            _fileFactory = new FileFactory();
        }

        [Fact]
        public void Create_ShouldSetName()
        {
            var viewModel = new AddFileViewModel {Name = "BGF"};
            var file = _fileFactory.Create(viewModel);
            Assert.Equal(viewModel.Name, file.Name);
        }

        [Fact]
        public void Create_ShouldSetType()
        {
            var viewModel = new AddFileViewModel {Type = "doc"};
            var file = _fileFactory.Create(viewModel);
            Assert.Equal(viewModel.Type, file.Type);
        }

        [Fact]
        public void Create_ShouldSetBytes()
        {
            var viewModel = new AddFileViewModel {Bytes = new byte[] {4, 3, 5, 6, 7, 8, 3, 5}};
            var file = _fileFactory.Create(viewModel);
            Assert.Equal(viewModel.Bytes, file.Bytes);
        }

        [Fact]
        public void Create_ShouldSetId()
        {
            var viewModel = new AddFileViewModel();
            var file = _fileFactory.Create(viewModel);
            Assert.NotEqual(Guid.Empty, Guid.Parse(file.Id));
        }
    }
}
