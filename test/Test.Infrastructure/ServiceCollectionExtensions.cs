using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceDescriptor GetDescriptor<TInterface, TImplementation>(this IServiceCollection services)
            where TImplementation : TInterface
        {
            return services.Where(s => s.ServiceType == typeof(TInterface))
                .SingleOrDefault(s => s.ImplementationType == typeof(TImplementation));
        }
    }
}
