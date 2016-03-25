using Newtonsoft.Json;

namespace Sogeti.Academy.Application.Presentations.Models
{
    public class File
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bytes")]
        public byte[] Bytes { get; set; }
    }
}
