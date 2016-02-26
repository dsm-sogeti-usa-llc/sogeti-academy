namespace Application.Test.Topics.Factories
{
    using Sogeti.Academy.Application.Topics.Factories;
    using Xunit;

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
