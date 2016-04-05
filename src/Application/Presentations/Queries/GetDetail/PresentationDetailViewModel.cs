namespace Sogeti.Academy.Application.Presentations.Queries.GetDetail
{
    public class PresentationDetailViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Topic { get; set; }
        public FileDetailViewModel[] Files { get; set; }
    }
}