using System.Linq;
using Sogeti.Academy.Infrastructure.Locator;
using Xunit;

namespace Sogeti.Academy.Infrastructure.Test.Locator
{
    public class LocatorTest
    {
        private readonly Locator<IDontDoAnything> _locator;

        public LocatorTest()
        {
            _locator = new Locator<IDontDoAnything>();
        }

        [Fact]
        public void Locate_ShouldCreateAllTypesOfInterface()
        {
            var instances = _locator.Locate(typeof(IDontDoAnything).Assembly).ToList();
            Assert.Equal(2, instances.Count);
            Assert.True(instances.Any(i => i.GetType() == typeof(DontDoAnything1)));
            Assert.True(instances.Any(i => i.GetType() == typeof(DontDoAnything2)));
        }
    }

    public interface IDontDoAnything
    {
        
    }

    public class DontDoAnything1 : IDontDoAnything
    {
        
    }

    public class DontDoAnything2 : IDontDoAnything
    {
        
    }
}
