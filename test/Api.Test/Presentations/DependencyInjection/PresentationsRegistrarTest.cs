using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Api.Presentations.DependencyInjection;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Commands.Edit;
using Sogeti.Academy.Application.Presentations.Commands.Remove;
using Sogeti.Academy.Application.Presentations.Factories;
using Sogeti.Academy.Application.Presentations.Queries.GetList;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Persistence.Presentations.Storage;
using Test.Infrastructure;
using Xunit;

namespace Sogeti.Academy.Api.Test.Presentations.DependencyInjection
{
    public class PresentationsRegistrarTest
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private readonly PresentationsRegistrar _presentationsRegistrar;

        public PresentationsRegistrarTest()
        {
            _configuration = new ConfigurationBuilder().Build();
            _services = new ServiceCollection();
            _presentationsRegistrar = new PresentationsRegistrar();
        }

        [Fact]
        public void RegisterServices_ShouldAddPresentationFactory()
        {
            Register();
            var descriptor = _services.GetDescriptor<IPresentationFactory, PresentationFactory>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void RegisterServices_ShouldAddFileFactory()
        {
            Register();
            var descriptor = _services.GetDescriptor<IFileFactory, FileFactory>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void RegisterServices_ShouldAddPresentationContext()
        {
            Register();
            var descriptor = _services.GetDescriptor<IPresentationContext, PresentationContext>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void RegisterServices_ShouldAddAddPresentationCommand()
        {
            Register();
            var descriptor = _services.GetDescriptor<IAddPresentationCommand, AddPresentationCommand>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void RegisterServices_ShouldAddEditPresentationCommand()
        {
            Register();
            var descriptor = _services.GetDescriptor<IEditPresentationCommand, EditPresentationCommand>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void RegisterServices_ShouldAddRemovePresentationCommand()
        {
            Register();
            var descriptor = _services.GetDescriptor<IRemovePresentationCommand, RemovePresentationCommand>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void RegisterServices_ShouldAddGetListQuery()
        {
            Register();
            var descriptor = _services.GetDescriptor<IGetListQuery, GetListQuery>();
            Assert.NotNull(descriptor);
        }

        private void Register()
        {
            _presentationsRegistrar.RegisterServices(_services, _configuration);
        }
    }
}
