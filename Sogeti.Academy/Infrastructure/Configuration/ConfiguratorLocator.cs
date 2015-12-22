using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sogeti.Academy.Infrastructure.Configuration
{
	public interface IConfiguratorLocator
	{
		List<IConfigurator> GetConfigurators(Assembly assembly);
	}

    public class ConfiguratorLocator : IConfiguratorLocator
    {
        public List<IConfigurator> GetConfigurators(Assembly assembly)
        {
            return assembly.GetTypes()
				.Where(t => t.GetInterface(typeof(IConfigurator).FullName) != null)
				.Select(t => Activator.CreateInstance(t))
				.Cast<IConfigurator>()
				.ToList();   
        }
    }
}