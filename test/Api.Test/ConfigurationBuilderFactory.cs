namespace Sogeti.Academy.Api.Test
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationBuilderFactory
    {
        public readonly static string BasePath = Path.Combine("..", "..", "src", "Api");

        public static IConfigurationBuilder Create()
        {
            return global::Test.Infrastructure.ConfigurationBuilderHelper.Create(BasePath);
        }
    }
}
