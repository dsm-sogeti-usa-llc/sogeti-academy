using System;
using System.Linq;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Topics.Models;
using Sogeti.Academy.Application.Topics.Storage;

namespace Sogeti.Academy.Application.Topics.Queries.GetList
{
	public interface IGetListQuery : IDisposable
	{
		Task<ListViewModel> Execute();
	}

    public class GetListQuery : IGetListQuery
    {
        private readonly ITopicsContext _topicsContext;
        
        public GetListQuery(ITopicsContext topicsContext)
        {
            _topicsContext = topicsContext;
        }

        public void Dispose()
        {
            _topicsContext.Dispose();
        }

        public async Task<ListViewModel> Execute()
        {
            var collection = _topicsContext.GetCollection<Topic>();
            var topics = await collection.GetAllAsync();
            var viewModels = topics
                .Select(t => new TopicViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Votes = t.Votes.Count
                })
                .OrderByDescending(t => t.Votes);
            return new ListViewModel 
            {
                Topics = viewModels.ToArray()
            };
        }
    }
}