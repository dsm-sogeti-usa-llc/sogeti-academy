using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Sogeti.Academy.Application.Topics.Queries.GetList;
using Sogeti.Academy.Mvc.General.Http;
using Sogeti.Academy.Mvc.Topics.Controllers;
using Xunit;

namespace Mvc.Test.Topics.Controllers
{
    public class TopicsControllerTest
    {
        private const string ApiUrl = "http://dumptruck.bb.com";
        private readonly Mock<IHttpClient> _httpClientMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TopicsController _topicsController;

        public TopicsControllerTest()
        {
            _httpClientMock = new Mock<IHttpClient>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpClientFactoryMock.Setup(s => s.Create()).Returns(_httpClientMock.Object);

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(s => s["Topics:ApiUrl"]).Returns(ApiUrl);

            _topicsController = new TopicsController(_httpClientFactoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task Index_ShouldGetListViewModelFromApi()
        {
            var expected = new ListViewModel();
            _httpClientMock.Setup(s => s.GetJson<ListViewModel>($"{ApiUrl}/topics"))
                .ReturnsAsync(expected);

            var result = (ViewResult)await _topicsController.Index();
            Assert.Same(expected, result.ViewData.Model);
        }

        [Fact]
        public async Task Index_ShouldSetTitle()
        {
            var result = (ViewResult) await _topicsController.Index();
            Assert.Equal("Topics", result.ViewData["Title"]);
        }

        [Fact]
        public async Task Index_ShouldDisposeClient()
        {
            await _topicsController.Index();
            _httpClientMock.Verify(s => s.Dispose(), Times.Once());
        }
    }
}
