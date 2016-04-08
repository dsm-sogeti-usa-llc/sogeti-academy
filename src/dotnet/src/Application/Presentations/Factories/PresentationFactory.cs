using System;
using System.Collections.Generic;
using System.Linq;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Models;

namespace Sogeti.Academy.Application.Presentations.Factories
{
    public interface IPresentationFactory
    {
        Presentation Create(AddPresentationViewModel viewModel);
    }

    public class PresentationFactory : IPresentationFactory
    {
        private readonly IFileFactory _fileFactory;

        public PresentationFactory()
            : this(new FileFactory())
        {
            
        }

        public PresentationFactory(IFileFactory fileFactory)
        {
            _fileFactory = fileFactory;
        }

        public Presentation Create(AddPresentationViewModel viewModel)
        {
            return new Presentation
            {
                Id = Guid.NewGuid().ToString(),
                Topic = viewModel.Topic,
                Description = viewModel.Description,
                Files = CreateFiles(viewModel.Files)
            };
        }

        private List<File> CreateFiles(AddFileViewModel[] files)
        {
            if (files == null)
                return new List<File>();

            return files.Select(f => _fileFactory.Create(f)).ToList();
        }
    }
}
