using System.IO;
using Sogeti.Academy.Application.Presentations.ViewModels;

namespace Sogeti.Academy.Application.Presentations.Commands.Add
{
    public class AddFileViewModel : IFileViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Bytes { get; set; }

        public Stream GetAsStream()
        {
            return Bytes == null ? new MemoryStream() : new MemoryStream(Bytes);
        }
    }
}
