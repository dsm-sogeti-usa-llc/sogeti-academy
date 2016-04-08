using Sogeti.Academy.Application.Topics.Factories;
using Xunit;

namespace Sogeti.Academy.Application.Test.Topics.Factories
{
    public class VoteFactoryTest
    {
        private readonly VoteFactory _voteFactory;

        public VoteFactoryTest()
        {
            _voteFactory = new VoteFactory();
        }

        [Fact]
        public void Create_ShouldSetVoteEmail()
        {
            var vote = _voteFactory.Create("bob");
            Assert.Equal("bob", vote.Email);
        }
    }
}
