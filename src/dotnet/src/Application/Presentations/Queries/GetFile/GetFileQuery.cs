using System;
using System.Linq;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;

namespace Sogeti.Academy.Application.Presentations.Queries.GetFile
{
    public interface IGetFileQuery 
    {
        Task<FileDownloadViewModel> Execute(string presentationId, string fileId);
    }

    public class GetFileQuery : IGetFileQuery
    {
        private readonly IPresentationContext _presentationContext;

        public GetFileQuery(IPresentationContext presentationContext)
        {
            _presentationContext = presentationContext;
        }

        public async Task<FileDownloadViewModel> Execute(string presentationId, string fileId)
        {
            var collection = _presentationContext.GetCollection<Presentation>();
            var presentation = await collection.GetByIdAsync(presentationId);
            var file = presentation.Files.Single(f => f.Id == fileId);
            return new FileDownloadViewModel
            {
                Name = file.Name,
                Type = file.Type,
                FileId = file.Id,
                PresentationId = presentation.Id,
                Bytes = file.Bytes
            };
        }
    }
}