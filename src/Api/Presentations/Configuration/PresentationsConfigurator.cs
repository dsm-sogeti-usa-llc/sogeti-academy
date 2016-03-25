using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Infrastructure.Configuration;

namespace Sogeti.Academy.Api.Presentations.Configuration
{
    public class PresentationsConfigurator : IConfigurator
    {
        public void Configure(IConfigurationBuilder builder)
        {
            builder.AddJsonFile(Path.Combine("Presentations", "Configuration", "presentations.json"));
        }
    }
}
