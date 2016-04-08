using System;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Presentations.Factories;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Application.Presentations.Storage;

namespace Sogeti.Academy.Application.Presentations.Commands.Add
{
    public interface IAddPresentationCommand
    {
        Task<string> Execute(AddPresentationViewModel viewModel);
    }

    public class AddPresentationCommand : IAddPresentationCommand
    {
        private readonly IPresentationContext _presentationContext;
        private readonly IPresentationFactory _presentationFactory;

        public AddPresentationCommand(IPresentationContext context)
            : this(context, new PresentationFactory())
        {
            
        }

        public AddPresentationCommand(IPresentationContext presentationContext, IPresentationFactory presentationFactory)
        {
            _presentationContext = presentationContext;
            _presentationFactory = presentationFactory;
        }

        public async Task<string> Execute(AddPresentationViewModel viewModel)
        {
            var presentation = _presentationFactory.Create(viewModel);
            var presentations = _presentationContext.GetCollection<Presentation>();
            return await presentations.CreateAsync(presentation);
        }
    }
}