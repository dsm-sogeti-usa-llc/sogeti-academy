using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.ModelBinding;
using Sogeti.Academy.Application.Presentations.Commands.Add;

namespace Sogeti.Academy.Api.Presentations.ModelBinders
{
    public class AddPresentationViewModelBinder : IModelBinder
    {
        public async Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            var formCollection = await bindingContext.OperationBindingContext.HttpContext.Request.ReadFormAsync();
            var viewModel = await MapToViewModel(formCollection);
            return ModelBindingResult.Success(bindingContext.FieldName, viewModel);
        }

        private async Task<AddPresentationViewModel> MapToViewModel(IFormCollection formCollection)
        {
            return new AddPresentationViewModel
            {
                Topic = formCollection.GetStringOrNull("topic"),
                Description = formCollection.GetStringOrNull("description"),
                Files = await MapFilesToViewModels(formCollection)
            };
        }

        private async Task<AddFileViewModel[]> MapFilesToViewModels(IFormCollection formColletion)
        {
            var viewModelTasks = formColletion.Files.Select(MapFileToViewModel);
            var viewModels = await Task.WhenAll(viewModelTasks);
            return viewModels;
        }

        private async Task<AddFileViewModel> MapFileToViewModel(IFormFile file)
        {
            var bytes = new byte[file.Length];
            await file.OpenReadStream().ReadAsync(bytes, 0, (int)file.Length);
            return new AddFileViewModel
            {
                Type = file.ContentType,
                Bytes = bytes,
                Name = file.GetFilename()
            };
        }
    }
}
