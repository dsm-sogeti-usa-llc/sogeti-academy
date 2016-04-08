using Moq;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Persistence.Presentations.Storage;
using Xunit;

namespace Sogeti.Academy.Persistence.Test.Presentations
{
    public class PresentationContextTest
    {
        private readonly PresentationContext _presentationContext;

        public PresentationContextTest()
        {
            var blobStorageMock = new Mock<IPresentationBlobStorage>();
            var configurationMock = new Mock<IConfiguration>();
            _presentationContext = new PresentationContext(configurationMock.Object, blobStorageMock.Object);
        }

        [Fact]
        public void GetPresentationCollection_ShouldGetPresentationCollection()
        {
            var collection = _presentationContext.GetCollection<Presentation>();
            Assert.IsType<PresentationCollection>(collection);
        }
    }
}
