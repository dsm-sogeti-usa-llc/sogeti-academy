using System.Collections.Generic;
using Newtonsoft.Json;
using Sogeti.Academy.Infrastructure.Models;
using Sogeti.Academy.Infrastructure.Storage;

namespace Sogeti.Academy.Application.Topics.Models
{
	[Document("Academy", "Topics")]
	public class Topic : IModel<string>
	{
        [JsonProperty("id")]
		public string Id { get; set; }

        [JsonProperty("name")]
		public string Name { get; set; }

        [JsonProperty("votes")]
		public List<Vote> Votes { get; set; }

	    public Topic()
	    {
	        Votes = new List<Vote>();
	    }
	}
}