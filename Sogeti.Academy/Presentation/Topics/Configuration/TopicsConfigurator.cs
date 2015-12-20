using System.IO;
using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Infrastructure.Configuration;

namespace Sogeti.Academy.Presentation.Topics.Configuration
{
    public class TopicsConfigurator : IConfigurator
    {
        public void Configure(ConfigurationBuilder builder)
        {
            builder.AddJsonFile(Path.Combine("Presentation", "Topics", "Configuration", "topics.json"));
        }
    }
}