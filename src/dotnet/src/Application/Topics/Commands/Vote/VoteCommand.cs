using System.Threading.Tasks;
using Sogeti.Academy.Application.Topics.Factories;
using Sogeti.Academy.Application.Topics.Models;
using Sogeti.Academy.Application.Topics.Storage;

namespace Sogeti.Academy.Application.Topics.Commands.Vote
{
    public interface IVoteCommand 
	{
		Task Execute(VoteViewModel viewModel);
	}

    public class VoteCommand : IVoteCommand
    {
		private readonly ITopicsContext _toipcsContext;
		private readonly IVoteFactory _voteFactory;

        public VoteCommand(ITopicsContext topicsContext)
            : this(topicsContext, new VoteFactory())
        {
            
        }

		public VoteCommand(ITopicsContext topicsContext, IVoteFactory voteFactory)
		{
			_toipcsContext = topicsContext;
			_voteFactory = voteFactory;
		}

        public async Task Execute(VoteViewModel viewModel)
        {
            var vote = _voteFactory.Create(viewModel.Email);
            var collection = _toipcsContext.GetCollection<Topic>();
            var topic = await collection.GetByIdAsync(viewModel.TopicId);
            topic.Votes.Add(vote);
            await collection.UpdateAsync(topic);
        }
    }
}