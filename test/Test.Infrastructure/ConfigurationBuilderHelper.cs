namespace Test.Infrastructure
{
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationBuilderHelper
    {
        public static IConfigurationBuilder Create(string basePath)
        {
            return new ConfigurationBuilder().SetBasePath(basePath);
        }
    }
}
