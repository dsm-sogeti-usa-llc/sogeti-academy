using System.IO;

namespace Sogeti.Academy.Application.Presentations.ViewModels
{
    public interface IFileViewModel
    {
        string Name { get; set; }
        string Type { get; set; }
        byte[] Bytes { get; set; }

        Stream GetAsStream();
    }
}
