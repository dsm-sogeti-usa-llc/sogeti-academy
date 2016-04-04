using System;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;

namespace Sogeti.Academy.Application.Presentations.Commands.Remove
{
    public interface IRemovePresentationCommand
    {
        Task Execute(RemovePresentationViewModel viewModel);
    }

    public class RemovePresentationCommand : IRemovePresentationCommand
    {
        private readonly IPresentationContext _presentationContext;

        public RemovePresentationCommand(IPresentationContext presentationContext)
        {
            _presentationContext = presentationContext;
        }
        
        public async Task Execute(RemovePresentationViewModel viewModel)
        {
            var collection = _presentationContext.GetCollection<Presentation>();
            await collection.RemoveById(viewModel.Id);
        }
    }
}