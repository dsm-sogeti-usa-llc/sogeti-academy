using System.IO;
using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Infrastructure.Configuration;

namespace Sogeti.Academy.Api.Topics.Configuration
{
    public class TopicsConfigurator : IConfigurator
    {
        public void Configure(ConfigurationBuilder builder)
        {
            builder.AddJsonFile(Path.Combine("Api", "Topics", "Configuration", "topics.json"));
        }
    }
}