using Sogeti.Academy.Mvc.General.Http;
using Xunit;

namespace Mvc.Test.General.Http
{
    public class HttpClientFactoryTest
    {
        private readonly HttpClientFactory _httpClientFactory;

        public HttpClientFactoryTest()
        {
            _httpClientFactory = new HttpClientFactory();
        }

        [Fact]
        public void Create_ShouldCreateHttpClient()
        {
            var client = _httpClientFactory.Create();
            Assert.IsType<HttpClient>(client);
        }
    }
}
