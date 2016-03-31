namespace Application.Test.Topics.Commands.Create
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using Sogeti.Academy.Application.Storage;
    using Sogeti.Academy.Application.Topics.Commands.Create;
    using Sogeti.Academy.Application.Topics.Factories;
    using Sogeti.Academy.Application.Topics.Models;
    using Sogeti.Academy.Application.Topics.Storage;
    using Xunit;

    public class CreateTopicCommandTest
    {
        private readonly Mock<IDocumentCollection<Topic>> _topicCollectionMock;
        private readonly Mock<ITopicsContext> _topicsContextMock;
        private readonly Mock<ITopicFactory> _topicFactoryMock;
        private readonly CreateTopicCommand _createTopicCommand;

        public CreateTopicCommandTest()
        {
            _topicFactoryMock = new Mock<ITopicFactory>();

            _topicCollectionMock = new Mock<IDocumentCollection<Topic>>();
            _topicsContextMock = new Mock<ITopicsContext>();
            _topicsContextMock.Setup(s => s.GetCollection<Topic>()).Returns(_topicCollectionMock.Object);

            _createTopicCommand = new CreateTopicCommand(_topicsContextMock.Object, _topicFactoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCreateTopicForTopicsCollection()
        {
            var newTopic = new Topic();

            var viewModel = new CreateTopicViewModel();
            _topicFactoryMock.Setup(s => s.Create(viewModel.Name)).Returns(newTopic);

            var id = Guid.NewGuid().ToString();
            _topicCollectionMock.Setup(s => s.CreateAsync(newTopic)).ReturnsAsync(id);

            var actual = await _createTopicCommand.Execute(viewModel);
            Assert.Equal(id, actual);
        }

        [Fact]
        public void Dispose_ShouldDisposeOfContext()
        {
            _createTopicCommand.Dispose();
            _topicsContextMock.Verify(s => s.Dispose(), Times.Once());
        }
    }
}
