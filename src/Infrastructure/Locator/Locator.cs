using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sogeti.Academy.Infrastructure.Locator
{
    public interface ILocator<T>
    {
        IEnumerable<T> Locate(Assembly assembly);
    }

    public class Locator<T> : ILocator<T>
    {
        public IEnumerable<T> Locate(Assembly assembly)
        {
            if (!typeof(T).IsInterface)
                throw new NotSupportedException($"{typeof(T).FullName} is not an interface.");

            return assembly.GetTypes()
                .Where(t => t.GetInterface(typeof(T).FullName) != null)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}
