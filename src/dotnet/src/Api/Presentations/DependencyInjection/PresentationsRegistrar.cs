using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Commands.Edit;
using Sogeti.Academy.Application.Presentations.Commands.Remove;
using Sogeti.Academy.Application.Presentations.Factories;
using Sogeti.Academy.Application.Presentations.Queries.GetDetail;
using Sogeti.Academy.Application.Presentations.Queries.GetFile;
using Sogeti.Academy.Application.Presentations.Queries.GetList;
using Sogeti.Academy.Application.Presentations.Storage;
using Sogeti.Academy.Infrastructure.DependencyInjection;
using Sogeti.Academy.Persistence.Presentations.Storage;

namespace Sogeti.Academy.Api.Presentations.DependencyInjection
{
    public class PresentationsRegistrar : IRegistrar
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPresentationFactory, PresentationFactory>();
            services.AddTransient<IFileFactory, FileFactory>();
            services.AddTransient<IPresentationContext, PresentationContext>();
            services.AddTransient<IAddPresentationCommand, AddPresentationCommand>();
            services.AddTransient<IEditPresentationCommand, EditPresentationCommand>();
            services.AddTransient<IRemovePresentationCommand, RemovePresentationCommand>();
            services.AddTransient<IGetListQuery, GetListQuery>();
            services.AddTransient<IGetDetailQuery, GetDetailQuery>();
            services.AddTransient<IGetFileQuery, GetFileQuery>();
        }
    }
}