using Newtonsoft.Json;

namespace Sogeti.Academy.Application.Topics.Models
{
	public class Vote
	{
        [JsonProperty("email")]
		public string Email { get; set; }
	}
}