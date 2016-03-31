using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sogeti.Academy.Application.Topics.Commands.Create;
using Sogeti.Academy.Application.Topics.Commands.Vote;
using Sogeti.Academy.Application.Topics.Queries.GetList;

namespace Sogeti.Academy.Api.Topics.Controllers
{
	[Route("topics")]
	public class TopicsController : Controller
	{
		private readonly IGetListQuery _getListQuery;
		private readonly ICreateTopicCommand _createTopicCommand;
		private readonly IVoteCommand _topicVoteCommand;
		
		public TopicsController(IGetListQuery getListQuery, 
			ICreateTopicCommand createTopicCommand, 
			IVoteCommand topicVoteCommand)
		{
			_getListQuery = getListQuery;
			_createTopicCommand = createTopicCommand;
			_topicVoteCommand = topicVoteCommand;
		}

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
			var viewModel = await _getListQuery.Execute();
            return Ok(viewModel);
        }
		
		[HttpPost("")]
		public async Task<IActionResult> Create(CreateTopicViewModel viewModel)
		{
			var id = await _createTopicCommand.Execute(viewModel);
			return Ok(id);
		}		
		
		[HttpPost("{id}/vote")]
		public async Task<IActionResult> Vote([FromBody]VoteViewModel viewModel)
		{
            await _topicVoteCommand.Execute(viewModel);
			return Ok();
		}
    }
}