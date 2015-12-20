using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sogeti.Academy.Infrastructure.DependencyInjection
{
	public interface IRegistrarLocator
	{
		List<IRegistrar> GetRegistrars(Assembly assembly);
	}

    public class RegistrarLocator : IRegistrarLocator
    {
        public List<IRegistrar> GetRegistrars(Assembly assembly)
        {
            return assembly.GetTypes()
				.Where(t => t.GetInterface(typeof(IRegistrar).FullName) != null)
				.Select(t => Activator.CreateInstance(t))
				.Cast<IRegistrar>()
				.ToList();
        }
    }
}