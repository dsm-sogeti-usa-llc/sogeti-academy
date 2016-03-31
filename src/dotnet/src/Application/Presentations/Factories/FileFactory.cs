using System;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.ViewModels;

namespace Sogeti.Academy.Application.Presentations.Factories
{
    public interface IFileFactory
    {
        File Create(IFileViewModel viewModel);
    }

    public class FileFactory : IFileFactory
    {
        public File Create(IFileViewModel viewModel)
        {
            return new File
            {
                Id = Guid.NewGuid().ToString(),
                Name = viewModel.Name,
                Bytes = viewModel.Bytes,
                Type = viewModel.Type
            };
        }
    }
}
