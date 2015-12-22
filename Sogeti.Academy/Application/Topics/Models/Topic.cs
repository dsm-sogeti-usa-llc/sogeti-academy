using System.Collections.Generic;
using Sogeti.Academy.Infrastructure.Models;
using Sogeti.Academy.Infrastructure.Storage;

namespace Sogeti.Academy.Application.Topics.Models
{
	[Document("Academy", "Topics")]
	public class Topic : IModel<string>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public List<Vote> Votes { get; set; }

	    public Topic()
	    {
	        Votes = new List<Vote>();
	    }
	}
}