using System.IO;
using Microsoft.Extensions.Configuration;

namespace Mvc.Test
{
    public static class ConfigurationBuilderFactory
    {
        public static readonly string BasePath = Path.Combine("..", "..", "src", "Mvc");

        public static IConfigurationBuilder Create()
        {
            return global::Test.Infrastructure.ConfigurationBuilderHelper.Create(BasePath);
        }
    }
}
