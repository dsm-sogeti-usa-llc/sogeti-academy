namespace Sogeti.Academy.Application.Presentations.Commands.Add
{
    public class AddPresentationViewModel
    {
        public string Topic { get; set; }
        public string Description { get; set; }

        public AddFileViewModel[] Files { get; set; }
    }
}
