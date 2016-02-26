namespace Sogeti.Academy.Api.Test.Topics.DependencyInjection
{
    using Api.Topics.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using Application.Topics.Commands.Create;
    using Application.Topics.Commands.Vote;
    using Application.Topics.Factories;
    using Application.Topics.Queries.GetList;
    using Application.Topics.Storage;
    using Persistence.Topics.Storage;
    using Xunit;

    public class TopicsRegistrarTest
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _serviceCollection;
        private readonly TopicsRegistrar _topicsRegistrar;

        public TopicsRegistrarTest()
        {
            _configuration = new ConfigurationBuilder().Build();
            _serviceCollection = new ServiceCollection();
            _topicsRegistrar = new TopicsRegistrar();
        }

        [Fact]
        public void Register_ShouldAddTopicFactory()
        {
            Register();
            
            var descriptor = GetDescriptor<ITopicFactory, TopicFactory>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddVoteFactory()
        {
            Register();

            var descriptor = GetDescriptor<IVoteFactory, VoteFactory>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddCreateTopicCommand()
        {
            Register();

            var descriptor = GetDescriptor<ICreateTopicCommand, CreateTopicCommand>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddVoteCommand()
        {
            Register();

            var descriptor = GetDescriptor<IVoteCommand, VoteCommand>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddGetListQuery()
        {
            Register();

            var descriptor = GetDescriptor<IGetListQuery, GetListQuery>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddTopicsContext()
        {
            Register();

            var descriptor = GetDescriptor<ITopicsContext, TopicsContext>();
            Assert.NotNull(descriptor);
        }

        private ServiceDescriptor GetDescriptor<TInterface, TImplementation>()
        {
            return _serviceCollection.Where(s => s.ServiceType == typeof(TInterface))
                .SingleOrDefault(s => s.ImplementationType == typeof(TImplementation));
        }

        private void Register()
        {
            _topicsRegistrar.RegisterServices(_serviceCollection, _configuration);
        }
    }
}
