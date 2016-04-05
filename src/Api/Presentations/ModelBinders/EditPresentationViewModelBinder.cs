using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.ModelBinding;
using Sogeti.Academy.Application.Presentations.Commands.Edit;

namespace Sogeti.Academy.Api.Presentations.ModelBinders
{
    public class EditPresentationViewModelBinder : IModelBinder
    {
        public async Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            var formCollection = await bindingContext.OperationBindingContext.HttpContext.Request.ReadFormAsync();
            var viewModel = await MapToViewModel(formCollection);
            return ModelBindingResult.Success(bindingContext.FieldName, viewModel);
        }

        private async Task<EditPresentationViewModel> MapToViewModel(IFormCollection formCollection)
        {
            return new EditPresentationViewModel
            {
                Id = formCollection.GetStringOrNull("id"),
                Topic = formCollection.GetStringOrNull("topic"),
                Description = formCollection.GetStringOrNull("description"),
                Files = await MapFilesToViewModels(formCollection)
            };
        }



        private static async Task<EditFileViewModel[]> MapFilesToViewModels(IFormCollection formCollection)
        {
            var existingFiles = MapExistingFiles(formCollection);
            var tasks = formCollection.Files.Select(f => f.AsViewModel<EditFileViewModel>()).ToArray();
            var newFiles = await Task.WhenAll(tasks);
            return existingFiles.Concat(newFiles).ToArray();
        }

        private static EditFileViewModel[] MapExistingFiles(IFormCollection formCollection)
        {
            return formCollection.Keys.Where(k => k.Contains("files"))
                .GroupBy(GetGroupKey)
                .Select(g => MapExistingFile(g, formCollection))
                .ToArray();
        }

        private static EditFileViewModel MapExistingFile(IGrouping<string, string> grouping, IFormCollection form)
        {
            var idKey = grouping.FirstOrDefault(v => v.Contains("id"));
            var nameKey = grouping.FirstOrDefault(v => v.Contains("name"));
            var typeKey = grouping.FirstOrDefault(v => v.Contains("type"));
            return new EditFileViewModel
            {
                Id = form.GetStringOrNull(idKey),
                Name = form.GetStringOrNull(nameKey),
                Type = form.GetStringOrNull(typeKey)
            };
        }

        private static string GetGroupKey(string key)
        {
            var startIndex = key.IndexOf("[");
            var length = key.IndexOf("]") - startIndex + 1;
            return key.Substring(startIndex, length);
        }
    }
}
