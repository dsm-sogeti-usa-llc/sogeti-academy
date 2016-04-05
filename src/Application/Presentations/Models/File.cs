using Newtonsoft.Json;

namespace Sogeti.Academy.Application.Presentations.Models
{
    public class File
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }

        [JsonIgnore]
        public byte[] Bytes { get; set; }
    }
}
