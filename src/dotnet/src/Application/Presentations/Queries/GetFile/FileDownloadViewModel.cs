namespace Sogeti.Academy.Application.Presentations.Queries.GetFile
{
    public class FileDownloadViewModel
    {
        public string FileId { get; set; }
        public string PresentationId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Bytes { get; set; }
    }
}