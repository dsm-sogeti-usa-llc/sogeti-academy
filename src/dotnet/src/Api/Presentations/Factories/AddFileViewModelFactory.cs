using System.Linq;
using System.Net.Http;
using Sogeti.Academy.Application.Presentations.Commands.Add;

namespace Sogeti.Academy.Api.Presentations.Factories
{
    public interface IAddFileViewModelFactory
    {
        AddFileViewModel[] Create(MultipartFormDataStreamProvider provider);
    }

    public class AddFileViewModelFactory : IAddFileViewModelFactory
    {
        public AddFileViewModel[] Create(MultipartFormDataStreamProvider provider)
        {
            return provider.FileData.Select(GetFileViewModel).ToArray();
        }

        private static AddFileViewModel GetFileViewModel(MultipartFileData fileData)
        {
            return new AddFileViewModel
            {
                Name = fileData.Headers.ContentDisposition.FileName,
                Type = fileData.Headers.ContentType.MediaType,
                Bytes = System.IO.File.ReadAllBytes(fileData.LocalFileName)
            };
        }
    }
}
