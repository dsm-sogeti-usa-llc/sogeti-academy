using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Sogeti.Academy.Api.General.Results;
using Sogeti.Academy.Api.Presentations.ModelBinders;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Commands.Edit;
using Sogeti.Academy.Application.Presentations.Commands.Remove;
using Sogeti.Academy.Application.Presentations.Queries.GetDetail;
using Sogeti.Academy.Application.Presentations.Queries.GetFile;
using Sogeti.Academy.Application.Presentations.Queries.GetList;
using Sogeti.Academy.Persistence.Presentations.Storage;

namespace Sogeti.Academy.Api.Presentations.Controllers
{
    [RoutePrefix("presentations")]
    public class PresentationsController : ApiController
    {
        private readonly IGetListQuery _getListQuery;
        private readonly IGetDetailQuery _getDetailQuery;
        private readonly IGetFileQuery _getFileQuery;
        private readonly IAddPresentationCommand _addPresentationCommand;
        private readonly IEditPresentationCommand _editPresentationCommand;
        private readonly IRemovePresentationCommand _removePresentationCommand;

        public PresentationsController()
            : this(new GetListQuery(new PresentationContext()), 
                  new GetDetailQuery(new PresentationContext()), 
                  new GetFileQuery(new PresentationContext()), 
                  new AddPresentationCommand(new PresentationContext()), 
                  new EditPresentationCommand(new PresentationContext()), 
                  new RemovePresentationCommand(new PresentationContext()))  
        {
            
        }

        public PresentationsController(IGetListQuery getListQuery, IGetDetailQuery getDetailQuery, IGetFileQuery getFileQuery, IAddPresentationCommand addPresentationCommand, IEditPresentationCommand editPresentationCommand, IRemovePresentationCommand removePresentationCommand)
        {
            _getListQuery = getListQuery;
            _getDetailQuery = getDetailQuery;
            _getFileQuery = getFileQuery;
            _addPresentationCommand = addPresentationCommand;
            _editPresentationCommand = editPresentationCommand;
            _removePresentationCommand = removePresentationCommand;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var viewModel = await _getListQuery.Execute();
            return Ok(viewModel);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetDetail(string id)
        {
            var viewModel = await _getDetailQuery.Execute(id);
            return Ok(viewModel);
        }

        [HttpGet]
        [Route("{presentationId}/files/{fileId}")]
        public async Task<IHttpActionResult> GetFile(string presentationId, string fileId)
        {
            var viewModel = await _getFileQuery.Execute(presentationId, fileId);
            return new FileResult(viewModel);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([ModelBinder(BinderType = typeof(AddPresentationViewModelBinder))] AddPresentationViewModel viewModel)
        {
            var id = await _addPresentationCommand.Execute(viewModel);
            return Ok(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Edit([ModelBinder(BinderType = typeof(EditPresentationViewModelBinder))] EditPresentationViewModel viewModel)
        {
            await _editPresentationCommand.Execute(viewModel);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Remove(RemovePresentationViewModel viewModel)
        {
            await _removePresentationCommand.Execute(viewModel);
            return Ok();
        }
    }
}