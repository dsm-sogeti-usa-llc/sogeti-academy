using Microsoft.Extensions.Configuration;
using Moq;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Persistence.Presentations.Storage;
using Xunit;

namespace Persistence.Test.Presentations
{
    public class PresentationContextTest
    {
        private readonly PresentationContext _presentationContext;

        public PresentationContextTest()
        {
            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var blobStorageMock = new Mock<IPresentationBlobStorage>();
            _presentationContext = new PresentationContext(configuration, blobStorageMock.Object);
        }

        [Fact]
        public void GetPresentationCollection_ShouldGetPresentationCollection()
        {
            var collection = _presentationContext.GetCollection<Presentation>();
            Assert.IsType<PresentationCollection>(collection);
        }
    }
}
