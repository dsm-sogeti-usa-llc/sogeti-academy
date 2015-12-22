using System.Threading.Tasks;
using Sogeti.Academy.Application.Topics.Models;
using Sogeti.Academy.Application.Topics.Storage;

namespace Sogeti.Academy.Application.Topics.Commands.Update
{
	public interface IUpdateTopicCommand
	{
		Task Execute(UpdateTopicViewModel viewModel);
	}

    public class UpdateTopicCommand : IUpdateTopicCommand
    {
		private readonly ITopicsContext _topicsContext;
		
		public UpdateTopicCommand(ITopicsContext topicsContext)
		{
			_topicsContext = topicsContext;
		}
		
        public async Task Execute(UpdateTopicViewModel viewModel)
        {
            var collection = _topicsContext.GetCollection<Topic>();
			var topic = await collection.GetByIdAsync(viewModel.Id);
			topic.Name = viewModel.Name;
			await collection.UpdateAsync(topic);
        }
    }
}