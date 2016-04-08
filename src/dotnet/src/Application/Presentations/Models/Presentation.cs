using System.Collections.Generic;
using Sogeti.Academy.Infrastructure.Models;
using Sogeti.Academy.Infrastructure.Storage;

namespace Sogeti.Academy.Application.Presentations.Models
{
    [Document("Academy", "Presentations")]
    public class Presentation : IModel<string>
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public List<File> Files { get; set; }

        public Presentation()
        {
            Files = new List<File>();
        }
    }
}
