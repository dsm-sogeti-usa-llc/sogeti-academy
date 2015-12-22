using Sogeti.Academy.Application.Topics.Models;

namespace Sogeti.Academy.Application.Topics.Factories
{
	public interface IVoteFactory
	{
		Vote Create(string email);
	}

    public class VoteFactory : IVoteFactory
    {
        public Vote Create(string email)
        {
            return new Vote
			{
				Email = email	
			};
        }
    }
}