using System;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Topics.Factories;
using Sogeti.Academy.Application.Topics.Models;
using Sogeti.Academy.Application.Topics.Storage;

namespace Sogeti.Academy.Application.Topics.Commands.Create
{
	public interface ICreateTopicCommand : IDisposable
	{
		Task<string> Execute(CreateTopicViewModel viewModel);
	}

    public class CreateTopicCommand : ICreateTopicCommand
    {
		private readonly ITopicsContext _topicsContext;
        private readonly ITopicFactory _topicFactory;
		
		public CreateTopicCommand(ITopicsContext topicsContext, ITopicFactory topicFactory)
		{
			_topicsContext = topicsContext;
            _topicFactory = topicFactory;
		}
		
        public void Dispose()
        {
            _topicsContext.Dispose();
        }

        public Task<string> Execute(CreateTopicViewModel viewModel)
        {
            var topic = _topicFactory.Create(viewModel.Name);
            var collection = _topicsContext.GetCollection<Topic>();
            return collection.CreateAsync(topic);
        }
    }
}