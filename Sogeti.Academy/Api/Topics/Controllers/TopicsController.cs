using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sogeti.Academy.Application.Topics.Commands.Create;
using Sogeti.Academy.Application.Topics.Commands.Remove;
using Sogeti.Academy.Application.Topics.Commands.Update;
using Sogeti.Academy.Application.Topics.Commands.Vote;
using Sogeti.Academy.Application.Topics.Queries.GetList;

namespace Sogeti.Academy.Api.Topics.Controllers
{
	[Route("api/topics")]
	public class TopicsController : Controller
	{
		private readonly IGetListQuery _getListQuery;
		private readonly ICreateTopicCommand _createTopicCommand;
		private readonly IRemoveTopicCommand _removeTopicCommand;
		private readonly IUpdateTopicCommand _updateTopicCommand;
		private readonly IVoteCommand _topicVoteCommand;
		
		public TopicsController(IGetListQuery getListQuery, 
			ICreateTopicCommand createTopicCommand, 
			IRemoveTopicCommand removeTopicCommand,
			IUpdateTopicCommand updateTopicCommand,
			IVoteCommand topicVoteCommand)
		{
			_getListQuery = getListQuery;
			_createTopicCommand = createTopicCommand;
			_removeTopicCommand = removeTopicCommand;
			_updateTopicCommand = updateTopicCommand;
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
		
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(UpdateTopicViewModel viewModel)
		{
			await _updateTopicCommand.Execute(viewModel);
			return Ok();
		}
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(string id)
		{
			await _removeTopicCommand.Execute(id);
			return Ok();	
		}
		
		[HttpPost("{id}/vote")]
		public async Task<IActionResult> Vote(VoteViewModel viewModel)
		{
			await _topicVoteCommand.Execute(viewModel);
			return Ok();
		}
    }
}