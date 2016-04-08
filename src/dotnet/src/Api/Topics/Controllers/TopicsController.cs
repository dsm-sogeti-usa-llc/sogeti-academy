using System.Threading.Tasks;
using System.Web.Http;
using Sogeti.Academy.Application.Topics.Commands.Create;
using Sogeti.Academy.Application.Topics.Commands.Vote;
using Sogeti.Academy.Application.Topics.Queries.GetList;
using Sogeti.Academy.Persistence.Topics.Storage;

namespace Sogeti.Academy.Api.Topics.Controllers
{
	[RoutePrefix("topics")]
	public class TopicsController : ApiController
	{
		private readonly IGetListQuery _getListQuery;
		private readonly ICreateTopicCommand _createTopicCommand;
		private readonly IVoteCommand _topicVoteCommand;

	    public TopicsController()
            : this(new GetListQuery(new TopicsContext()), 
                  new CreateTopicCommand(new TopicsContext()), 
                  new VoteCommand(new TopicsContext()))
	    {
	        
	    }

		public TopicsController(IGetListQuery getListQuery, 
			ICreateTopicCommand createTopicCommand, 
			IVoteCommand topicVoteCommand)
		{
			_getListQuery = getListQuery;
			_createTopicCommand = createTopicCommand;
			_topicVoteCommand = topicVoteCommand;
		}

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
			var viewModel = await _getListQuery.Execute();
            return Ok(viewModel);
        }
		
		[HttpPost]
        [Route("")]
		public async Task<IHttpActionResult> Create(CreateTopicViewModel viewModel)
		{
			var id = await _createTopicCommand.Execute(viewModel);
			return Ok(id);
		}		
		
		[HttpPost]
        [Route("{id}/vote")]
		public async Task<IHttpActionResult> Vote([FromBody]VoteViewModel viewModel)
		{
            await _topicVoteCommand.Execute(viewModel);
			return Ok();
		}
    }
}