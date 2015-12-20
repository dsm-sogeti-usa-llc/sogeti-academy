using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Application.Topics.Storage;
using Sogeti.Academy.Persistence.Storage;

namespace Sogeti.Academy.Persistence.Topics.Storage
{
    public class TopicsContext : DocumentContext, ITopicsContext
	{
		public TopicsContext(IConfiguration configuration)
			: base(configuration["Topics:DocumentDbEndpiontUrl"], configuration["Topics:DocumentDbAuthKey"])
		{
			
		}
	}
}