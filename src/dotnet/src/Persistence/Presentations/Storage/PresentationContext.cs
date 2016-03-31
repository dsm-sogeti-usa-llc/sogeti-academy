using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Persistence.Storage;

namespace Sogeti.Academy.Persistence.Presentations.Storage
{
    public class PresentationContext : DocumentContext, IPresentationContext
    {
        public PresentationContext(IConfiguration configuration) 
            : base(configuration["Presentations:DocumentDbEndpiontUrl"], configuration["Presentations:DocumentDbAuthKey"])
        {
        }
    }
}
