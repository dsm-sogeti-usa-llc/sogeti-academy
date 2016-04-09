using System.Net.Http;
using System.Threading.Tasks;
using Sogeti.Academy.Api.General.Http;
using Sogeti.Academy.Application.Presentations.Commands.Edit;

namespace Sogeti.Academy.Api.Presentations.Factories
{
    public interface IEditPresentationViewModelFactory
    {
        Task<EditPresentationViewModel> Create(HttpRequestMessage request);
    }

    public class EditPresentationViewModelFactory : IEditPresentationViewModelFactory
    {
        private readonly IEditFileViewModelFactory _editFileViewModelFactory;
        private readonly MultipartFormDataProviderFactory _multipartFormDataProviderFactory;

        public EditPresentationViewModelFactory(IServer server)
            : this(server, new EditFileViewModelFactory())
        {
        }

        public EditPresentationViewModelFactory(IServer server, IEditFileViewModelFactory editFileViewModelFactory)
        {
            _multipartFormDataProviderFactory = new MultipartFormDataProviderFactory(server);
            _editFileViewModelFactory = editFileViewModelFactory;
        }


        public async Task<EditPresentationViewModel> Create(HttpRequestMessage request)
        {
            var provider = await _multipartFormDataProviderFactory.GetFormDataProvider(request);

            return new EditPresentationViewModel
            {
                Description = provider.FormData.GetStringOrDefault("description"),
                Id = provider.FormData.GetStringOrDefault("id"),
                Topic = provider.FormData.GetStringOrDefault("topic"),
                Files = _editFileViewModelFactory.Create(provider)
            };
        }
    }
}
