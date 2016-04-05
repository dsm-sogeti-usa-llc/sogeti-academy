using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Sogeti.Academy.Api.Presentations.ModelBinders;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Commands.Edit;
using Sogeti.Academy.Application.Presentations.Commands.Remove;
using Sogeti.Academy.Application.Presentations.Queries.GetDetail;
using Sogeti.Academy.Application.Presentations.Queries.GetFile;
using Sogeti.Academy.Application.Presentations.Queries.GetList;

namespace Sogeti.Academy.Api.Presentations.Controllers
{
    [Route("presentations")]
    public class PresentationsController : Controller
    {
        private readonly IGetListQuery _getListQuery;
        private readonly IGetDetailQuery _getDetailQuery;
        private readonly IGetFileQuery _getFileQuery;
        private readonly IAddPresentationCommand _addPresentationCommand;
        private readonly IEditPresentationCommand _editPresentationCommand;
        private readonly IRemovePresentationCommand _removePresentationCommand;

        public PresentationsController(IGetListQuery getListQuery, IGetDetailQuery getDetailQuery, IGetFileQuery getFileQuery, IAddPresentationCommand addPresentationCommand, IEditPresentationCommand editPresentationCommand, IRemovePresentationCommand removePresentationCommand)
        {
            _getListQuery = getListQuery;
            _getDetailQuery = getDetailQuery;
            _getFileQuery = getFileQuery;
            _addPresentationCommand = addPresentationCommand;
            _editPresentationCommand = editPresentationCommand;
            _removePresentationCommand = removePresentationCommand;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var viewModel = await _getListQuery.Execute();
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(string id)
        {
            var viewModel = await _getDetailQuery.Execute(id);
            return Ok(viewModel);
        }

        [HttpGet("{presentationId}/files/{fileId}")]
        public async Task<IActionResult> GetFile(string presentationId, string fileId)
        {
            var viewModel = await _getFileQuery.Execute(presentationId, fileId);
            return File(viewModel.Bytes, viewModel.Type, viewModel.Name);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([ModelBinder(BinderType = typeof(AddPresentationViewModelBinder))] AddPresentationViewModel viewModel)
        {
            var id = await _addPresentationCommand.Execute(viewModel);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([ModelBinder(BinderType = typeof(EditPresentationViewModelBinder))] EditPresentationViewModel viewModel)
        {
            await _editPresentationCommand.Execute(viewModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(RemovePresentationViewModel viewModel)
        {
            await _removePresentationCommand.Execute(viewModel);
            return Ok();
        }
    }
}