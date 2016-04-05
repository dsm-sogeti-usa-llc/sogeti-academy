namespace Sogeti.Academy.Application.Presentations.Commands.Edit
{
    public class EditPresentationViewModel
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public EditFileViewModel[] Files { get; set; }

        public EditPresentationViewModel()
        {
            Files = new EditFileViewModel[0];
        }
    }
}