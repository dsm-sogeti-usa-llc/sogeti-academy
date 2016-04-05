using Microsoft.Extensions.Configuration;

namespace Sogeti.Academy.Infrastructure.Configuration
{
	public interface IConfigurator
	{
		void Configure(IConfigurationBuilder builder);
	}
}