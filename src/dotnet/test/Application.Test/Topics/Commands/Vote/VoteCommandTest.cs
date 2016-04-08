using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Sogeti.Academy.Application.Storage;
using Sogeti.Academy.Application.Topics.Commands.Vote;
using Sogeti.Academy.Application.Topics.Factories;
using Sogeti.Academy.Application.Topics.Models;
using Sogeti.Academy.Application.Topics.Storage;
using Xunit;

namespace Sogeti.Academy.Application.Test.Topics.Commands.Vote
{
    public class VoteCommandTest
    {
        private readonly Mock<IDocumentCollection<Topic>> _topicCollectionMock;
        private readonly Mock<IVoteFactory> _voteFactoryMock;
        private readonly VoteCommand _voteCommand;

        public VoteCommandTest()
        {
            _topicCollectionMock = new Mock<IDocumentCollection<Topic>>();
            var topicsContextMock = new Mock<ITopicsContext>();
            topicsContextMock.Setup(s => s.GetCollection<Topic>()).Returns(_topicCollectionMock.Object);

            _voteFactoryMock = new Mock<IVoteFactory>();
            _voteCommand = new VoteCommand(topicsContextMock.Object, _voteFactoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldAddVoteToTopic()
        {
            var viewModel = new VoteViewModel
            {
                TopicId = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString()
            };

            var topic = new Topic {Votes = new List<Application.Topics.Models.Vote>()};
            _topicCollectionMock.Setup(s => s.GetByIdAsync(viewModel.TopicId)).ReturnsAsync(topic);

            var vote = new Application.Topics.Models.Vote();
            _voteFactoryMock.Setup(s => s.Create(viewModel.Email)).Returns(vote);

            await _voteCommand.Execute(viewModel);
            Assert.Contains(vote, topic.Votes);
        }

        [Fact]
        public async Task Execute_ShouldUpdateTopic()
        {
            var viewModel = new VoteViewModel {TopicId = Guid.NewGuid().ToString()};

            var topic = new Topic {Votes = new List<Application.Topics.Models.Vote>()};
            _topicCollectionMock.Setup(s => s.GetByIdAsync(viewModel.TopicId)).ReturnsAsync(topic);

            await _voteCommand.Execute(viewModel);
            _topicCollectionMock.Verify(s => s.UpdateAsync(topic), Times.Once());
        }
    }
}
