using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Sogeti.Academy.Infrastructure.Configuration
{
    public interface IConfiguration
    {
        string this[string key] { get; }
    }

    public class Configuration : IConfiguration
    {
        private readonly IDictionary _environmentVariables;

        public Configuration()
        {
            _environmentVariables = Environment.GetEnvironmentVariables();
        }

        public string this[string key]
        {
            get
            {
                if (_environmentVariables.Contains(key))
                    return _environmentVariables[key] as string;

                return ConfigurationManager.AppSettings[key];
            }
        }
    }
}
