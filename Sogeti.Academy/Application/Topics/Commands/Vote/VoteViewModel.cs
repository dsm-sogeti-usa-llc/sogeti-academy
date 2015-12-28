using Newtonsoft.Json;

namespace Sogeti.Academy.Application.Topics.Commands.Vote
{
    [JsonObject(MemberSerialization.OptOut)]
	public class VoteViewModel
	{
        [JsonProperty("topicId")]
        public string TopicId { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
	}
}