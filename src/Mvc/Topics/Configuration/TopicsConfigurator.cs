using System.IO;
using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Infrastructure.Configuration;

namespace Sogeti.Academy.Mvc.Topics.Configuration
{
    public class TopicsConfigurator : IConfigurator
    {
        public void Configure(ConfigurationBuilder builder)
        {
            builder.AddJsonFile(Path.Combine("Topics", "Configuration", "topics.json"));
        }
    }
}