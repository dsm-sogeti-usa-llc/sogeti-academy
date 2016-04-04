using System;
using System.Linq;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;

namespace Sogeti.Academy.Application.Presentations.Queries.GetList
{
    public interface IGetListQuery 
    {
        Task<ListViewModel> Execute();
    }

    public class GetListQuery : IGetListQuery
    {
        private readonly IPresentationContext _presentationContext;

        public GetListQuery(IPresentationContext presentationContext)
        {
            _presentationContext = presentationContext;
        }

        public async Task<ListViewModel> Execute()
        {
            var collection = _presentationContext.GetCollection<Presentation>();
            var presentations = await collection.GetAllAsync();
            var viewModels = presentations.Select(Map);
            return new ListViewModel
            {
                Presentations = viewModels.OrderBy(v => v.Topic).ToArray()
            };
        }

        private static PresentationViewModel Map(Presentation p)
        {
            return new PresentationViewModel
            {
                Id = p.Id,
                Topic = p.Topic,
                Description =  p.Description,
                FilesCount = p.Files?.Count ?? 0
            };
        }

    }
}
