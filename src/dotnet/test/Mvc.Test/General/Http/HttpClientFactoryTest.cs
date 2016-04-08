using Sogeti.Academy.Mvc.General.Http;
using Xunit;

namespace Sogeti.Academy.Mvc.Test.General.Http
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
