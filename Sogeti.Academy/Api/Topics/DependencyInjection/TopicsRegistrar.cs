using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Application.Topics.Commands.Create;
using Sogeti.Academy.Application.Topics.Commands.Vote;
using Sogeti.Academy.Application.Topics.Factories;
using Sogeti.Academy.Application.Topics.Queries.GetList;
using Sogeti.Academy.Application.Topics.Storage;
using Sogeti.Academy.Infrastructure.DependencyInjection;
using Sogeti.Academy.Persistence.Topics.Storage;

namespace Sogeti.Academy.Api.Topics.DependencyInjection
{
    public class TopicsRegistrar : IRegistrar
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITopicFactory, TopicFactory>();
            services.AddTransient<IVoteFactory, VoteFactory>();
            services.AddTransient<ICreateTopicCommand, CreateTopicCommand>();
            services.AddTransient<IVoteCommand, VoteCommand>();
            services.AddTransient<IGetListQuery, GetListQuery>();
            services.AddTransient<ITopicsContext, TopicsContext>();
        }
    }
}