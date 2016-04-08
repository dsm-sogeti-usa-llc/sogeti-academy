using Sogeti.Academy.Application.Topics.Storage;
using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Persistence.Storage;

namespace Sogeti.Academy.Persistence.Topics.Storage
{
    public class TopicsContext : DocumentContext, ITopicsContext
	{
        public TopicsContext()
            : this(new Configuration())
        {
            
        }

		public TopicsContext(IConfiguration configuration)
			: base(configuration["Topics:DocumentDbEndpiontUrl"], configuration["Topics:DocumentDbAuthKey"])
		{
			
		}
	}
}