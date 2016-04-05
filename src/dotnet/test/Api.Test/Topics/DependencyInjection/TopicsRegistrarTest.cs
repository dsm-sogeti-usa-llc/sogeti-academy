namespace Sogeti.Academy.Api.Test.Topics.DependencyInjection
{
    using Api.Topics.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Application.Topics.Commands.Create;
    using Application.Topics.Commands.Vote;
    using Application.Topics.Factories;
    using Application.Topics.Queries.GetList;
    using Application.Topics.Storage;
    using Persistence.Topics.Storage;
    using Xunit;
    using global::Test.Infrastructure;

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
            
            var descriptor = _serviceCollection.GetDescriptor<ITopicFactory, TopicFactory>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddVoteFactory()
        {
            Register();

            var descriptor = _serviceCollection.GetDescriptor<IVoteFactory, VoteFactory>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddCreateTopicCommand()
        {
            Register();

            var descriptor = _serviceCollection.GetDescriptor<ICreateTopicCommand, CreateTopicCommand>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddVoteCommand()
        {
            Register();

            var descriptor = _serviceCollection.GetDescriptor<IVoteCommand, VoteCommand>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddGetListQuery()
        {
            Register();

            var descriptor = _serviceCollection.GetDescriptor<IGetListQuery, GetListQuery>();
            Assert.NotNull(descriptor);
        }

        [Fact]
        public void Register_ShouldAddTopicsContext()
        {
            Register();

            var descriptor = _serviceCollection.GetDescriptor<ITopicsContext, TopicsContext>();
            Assert.NotNull(descriptor);
        }

        private void Register()
        {
            _topicsRegistrar.RegisterServices(_serviceCollection, _configuration);
        }
    }
}
