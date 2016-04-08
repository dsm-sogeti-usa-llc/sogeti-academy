using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using Sogeti.Academy.Application.Topics.Queries.GetList;
using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Mvc.General.Http;
using Sogeti.Academy.Mvc.Topics.Controllers;
using Xunit;

namespace Sogeti.Academy.Mvc.Test.Topics.Controllers
{
    public class TopicsControllerTest
    {
        private const string ApiUrl = "http://dumptruck.bb.com";
        private readonly Mock<IHttpClient> _httpClientMock;
        private readonly TopicsController _topicsController;

        public TopicsControllerTest()
        {
            _httpClientMock = new Mock<IHttpClient>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(s => s.Create()).Returns(_httpClientMock.Object);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(s => s["Topics:ApiUrl"]).Returns(ApiUrl);

            _topicsController = new TopicsController(httpClientFactoryMock.Object, configurationMock.Object);
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
