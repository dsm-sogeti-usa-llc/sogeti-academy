using System.Threading.Tasks;
using Sogeti.Academy.Application.Topics.Models;
using Sogeti.Academy.Application.Topics.Storage;

namespace Sogeti.Academy.Application.Topics.Commands.Remove
{
	public interface IRemoveTopicCommand
	{
		Task Execute(string id);
	}

    public class RemoveTopicCommand : IRemoveTopicCommand
    {
		private readonly ITopicsContext _topicsContext;
		
		public RemoveTopicCommand(ITopicsContext topicsContext)
		{
			_topicsContext = topicsContext;
		}
        public Task Execute(string id)
        {
           	var collection = _topicsContext.GetCollection<Topic>();
			return collection.RemoveAsync(id);
        }
    }
}