using System;
using System.Linq;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;

namespace Sogeti.Academy.Application.Presentations.Queries.GetDetail
{
    public interface IGetDetailQuery 
    {
        Task<PresentationDetailViewModel> Execute(string id);
    }

    public class GetDetailQuery : IGetDetailQuery
    {
        private readonly IPresentationContext _presentationContext;

        public GetDetailQuery(IPresentationContext presentationContext)
        {
            _presentationContext = presentationContext;
        }

        public async Task<PresentationDetailViewModel> Execute(string id)
        {
            var collection = _presentationContext.GetCollection<Presentation>();
            var presentation = await collection.GetByIdAsync(id);
            return new PresentationDetailViewModel
            {
                Id = presentation.Id,
                Description = presentation.Description,
                Topic = presentation.Topic,
                Files = presentation.Files.Select(Map).ToArray()
            };
        }
        
        private static FileDetailViewModel Map(File file)
        {
            return new FileDetailViewModel
            {
                Id = file.Id,
                Name = file.Name,
                Type = file.Type,
                Size = file.Size
            };
        }
    }
}