using System.Collections.Generic;
using Newtonsoft.Json;
using Sogeti.Academy.Infrastructure.Models;
using Sogeti.Academy.Infrastructure.Storage;

namespace Sogeti.Academy.Application.Presentations.Models
{
    [Document("Academy", "Presentations")]
    public class Presentation : IModel<string>
    {
        public Presentation()
        {
            Files = new List<File>();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("files")]
        public List<File> Files { get; set; }
    }
}
