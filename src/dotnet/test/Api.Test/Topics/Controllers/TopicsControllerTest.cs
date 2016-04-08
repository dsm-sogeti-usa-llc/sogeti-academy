using System.Web.Http.Results;

namespace Sogeti.Academy.Api.Test.Topics.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Api.Topics.Controllers;
    using Application.Topics.Commands.Create;
    using Application.Topics.Commands.Vote;
    using Application.Topics.Queries.GetList;
    using Moq;
    using Xunit;

    public class TopicsControllerTest
    {
        private readonly Mock<IGetListQuery> _getListQueryMock;
        private readonly Mock<ICreateTopicCommand> _createTopicCommandMock;
        private readonly Mock<IVoteCommand> _voteCommandMock;
        private readonly TopicsController _topicsController;

        public TopicsControllerTest()
        {
            _getListQueryMock = new Mock<IGetListQuery>();
            _createTopicCommandMock = new Mock<ICreateTopicCommand>();
            _voteCommandMock = new Mock<IVoteCommand>();
            _topicsController = new TopicsController(_getListQueryMock.Object, _createTopicCommandMock.Object, _voteCommandMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldExecuteGetListQuery()
        {
            var expected = new ListViewModel();
            _getListQueryMock.Setup(s => s.Execute()).ReturnsAsync(expected);

            var result = (OkNegotiatedContentResult<ListViewModel>)await _topicsController.GetAll();
            Assert.Same(expected, result.Content);
        }

        [Fact]
        public async Task Create_ShouldExecuteCreateCommand()
        {
            var viewModel = new CreateTopicViewModel();
            var expected = Guid.NewGuid().ToString();
            _createTopicCommandMock.Setup(s => s.Execute(viewModel))
                .ReturnsAsync(expected);

            var result = (OkNegotiatedContentResult<string>) await _topicsController.Create(viewModel);
            Assert.Equal(expected, result.Content);
        }

        [Fact]
        public async Task Vote_ShouldExecuteVoteCommand()
        {
            var viewModel = new VoteViewModel();
            _voteCommandMock.Setup(s => s.Execute(viewModel)).Returns(Task.FromResult(new object()));

            var result = await _topicsController.Vote(viewModel);
            _voteCommandMock.Verify(s => s.Execute(viewModel), Times.Once());
            Assert.IsType<OkResult>(result);
        }
    }
}
