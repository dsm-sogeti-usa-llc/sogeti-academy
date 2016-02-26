namespace Infrastructure.Test.Startup
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Mvc.Razor;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Sogeti.Academy.Infrastructure.Configuration;
    using Sogeti.Academy.Infrastructure.DependencyInjection;
    using Sogeti.Academy.Infrastructure.Locator;
    using Sogeti.Academy.Infrastructure.Pipeline;
    using Sogeti.Academy.Infrastructure.Startup;
    using Xunit;

    public class StartupServiceTest
    {
        private static readonly Assembly Assembly = typeof(StartupServiceTest).Assembly;
        private readonly StartupService _startupService;
        private readonly IServiceCollection _services;
        private readonly Mock<IApplicationBuilder> _applicationBuilderMock;
        private readonly Mock<ILocator<IConfigurator>> _configuratorLocatorMock;
        private readonly Mock<ILocator<IRegistrar>> _registrarLocatorMock;
        private readonly Mock<ILocator<IPipelineConfigurator>> _pipelineConfiguratorLocatorMock;
        private readonly Mock<ILocator<IViewLocationExpander>> _viewLocationExpanderLocatorMock;

        public StartupServiceTest()
        {
            _services = new ServiceCollection();
            _applicationBuilderMock = new Mock<IApplicationBuilder>();

            _configuratorLocatorMock = new Mock<ILocator<IConfigurator>>();
            _registrarLocatorMock = new Mock<ILocator<IRegistrar>>();
            _pipelineConfiguratorLocatorMock = new Mock<ILocator<IPipelineConfigurator>>();
            _viewLocationExpanderLocatorMock = new Mock<ILocator<IViewLocationExpander>>();
            _startupService = new StartupService(_configuratorLocatorMock.Object,
                _registrarLocatorMock.Object,
                _pipelineConfiguratorLocatorMock.Object,
                _viewLocationExpanderLocatorMock.Object,
                Assembly);
        }

        [Fact]
        public void Configuration_ShouldUseAllConfigurators()
        {
            var configurators = new List<Mock<IConfigurator>>
            {
                new Mock<IConfigurator>(),
                new Mock<IConfigurator>(),
                new Mock<IConfigurator>()
            };
            _configuratorLocatorMock.Setup(s => s.Locate(Assembly))
                .Returns(configurators.Select(c => c.Object));

            var configuration = _startupService.Configuration;
            Assert.NotNull(configuration);
            configurators[0].Verify(s => s.Configure(It.IsAny<IConfigurationBuilder>()), Times.Once());
            configurators[1].Verify(s => s.Configure(It.IsAny<IConfigurationBuilder>()), Times.Once());
            configurators[2].Verify(s => s.Configure(It.IsAny<IConfigurationBuilder>()), Times.Once());
        }

        [Fact]
        public void ConfigureServices_ShouldAddConfigurationInstance()
        {
            _startupService.ConfigureServices(_services);
            var hasConfigurationInstance = _services
                .Where(s => s.Lifetime == ServiceLifetime.Singleton)
                .Any(s => s.ImplementationInstance == _startupService.Configuration);
            Assert.True(hasConfigurationInstance);
        }

        [Fact]
        public void ConfigureServices_ShouldUseAllRegistrars()
        {
            var registrars = new List<Mock<IRegistrar>>
            {
                new Mock<IRegistrar>(),
                new Mock<IRegistrar>(),
                new Mock<IRegistrar>()
            };
            _registrarLocatorMock.Setup(s => s.Locate(Assembly))
                .Returns(registrars.Select(r => r.Object));

            _startupService.ConfigureServices(_services);
            registrars[0].Verify(s => s.RegisterServices(_services, _startupService.Configuration), Times.Once());
            registrars[1].Verify(s => s.RegisterServices(_services, _startupService.Configuration), Times.Once());
            registrars[2].Verify(s => s.RegisterServices(_services, _startupService.Configuration), Times.Once());
        }

        [Fact]
        public void Configure_ShouldUseAllPipelineConfigurators()
        {
            var pipelineConfigurators = new List<Mock<IPipelineConfigurator>>
            {
                new Mock<IPipelineConfigurator>(),
                new Mock<IPipelineConfigurator>(),
                new Mock<IPipelineConfigurator>()
            };
            _pipelineConfiguratorLocatorMock.Setup(s => s.Locate(Assembly))
                .Returns(pipelineConfigurators.Select(p => p.Object));

            _startupService.Configure(_applicationBuilderMock.Object);
            pipelineConfigurators[0].Verify(s => s.Configure(_applicationBuilderMock.Object), Times.Once());
            pipelineConfigurators[1].Verify(s => s.Configure(_applicationBuilderMock.Object), Times.Once());
            pipelineConfigurators[2].Verify(s => s.Configure(_applicationBuilderMock.Object), Times.Once());
        }

        [Fact]
        public void ConfigureViewLocations_ShouldUseAllViewLocationExpanders()
        {
            var viewLocationExpanders = new List<Mock<IViewLocationExpander>>
            {
                new Mock<IViewLocationExpander>(),
                new Mock<IViewLocationExpander>(),
                new Mock<IViewLocationExpander>()
            };
            _viewLocationExpanderLocatorMock.Setup(s => s.Locate(Assembly))
                .Returns(viewLocationExpanders.Select(p => p.Object));

            var options = new RazorViewEngineOptions();

            _startupService.ConfigureViewsLocations(options);
            Assert.Contains(viewLocationExpanders[0].Object, options.ViewLocationExpanders);
            Assert.Contains(viewLocationExpanders[1].Object, options.ViewLocationExpanders);
            Assert.Contains(viewLocationExpanders[2].Object, options.ViewLocationExpanders);
        }
    }
}
