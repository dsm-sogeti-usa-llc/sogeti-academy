using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Presentations.Factories;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;

namespace Sogeti.Academy.Application.Presentations.Commands.Edit
{
    public interface IEditPresentationCommand
    {
        Task Execute(EditPresentationViewModel viewModel);
    }

    public class EditPresentationCommand : IEditPresentationCommand
    {
        private readonly IPresentationContext _presentationContext;
        private readonly IFileFactory _fileFactory;

        public EditPresentationCommand(IPresentationContext presentationContext)
            : this(presentationContext, new FileFactory())
        {
            
        }

        public EditPresentationCommand(IPresentationContext presentationContext, IFileFactory fileFactory)
        {
            _presentationContext = presentationContext;
            _fileFactory = fileFactory;
        }

        public async Task Execute(EditPresentationViewModel viewModel)
        {
            var presentations = _presentationContext.GetCollection<Presentation>();

            var presentation = await presentations.GetByIdAsync(viewModel.Id);
            UpdatePresentation(presentation, viewModel);
            await presentations.UpdateAsync(presentation);
        }

        private void UpdatePresentation(Presentation presentation, EditPresentationViewModel viewModel)
        {
            presentation.Description = viewModel.Description;
            presentation.Topic = viewModel.Topic;

            foreach (var fileViewModel in viewModel.Files)
                UpdateFiles(fileViewModel, presentation.Files);

            for (var i = 0; i < presentation.Files.Count; i++)
                if (IsDeletedFile(presentation.Files[i], viewModel.Files))
                    presentation.Files.Remove(presentation.Files[i]);
        }

        private void UpdateFiles(EditFileViewModel viewModel, List<File> files)
        {
            if (IsNewFile(viewModel))
                files.Add(_fileFactory.Create(viewModel));

            else if (IsExistingFile(viewModel, files))
                UpdateFile(viewModel, files);
        }

        private static void UpdateFile(EditFileViewModel viewModel, List<File> files)
        {
            var file = files.Single(f => f.Id == viewModel.Id);
            file.Name = viewModel.Name;
            file.Type = viewModel.Type;

            if (viewModel.Bytes != null)
                file.Bytes = viewModel.Bytes;
        }

        private static bool IsNewFile(EditFileViewModel viewModel)
        {
            return string.IsNullOrEmpty(viewModel.Id);
        }

        private static bool IsExistingFile(EditFileViewModel viewModel, List<File> files)
        {
            return files.Any(f => f.Id == viewModel.Id);
        }

        private static bool IsDeletedFile(File file, EditFileViewModel[] viewModels)
        {
            return viewModels.All(v => v.Id != file.Id);
        }
    }
}