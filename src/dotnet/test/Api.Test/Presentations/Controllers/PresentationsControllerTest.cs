using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Moq;
using Sogeti.Academy.Api.Presentations.Controllers;
using Sogeti.Academy.Application.Presentations.Commands.Add;
using Sogeti.Academy.Application.Presentations.Commands.Edit;
using Sogeti.Academy.Application.Presentations.Commands.Remove;
using Sogeti.Academy.Application.Presentations.Queries.GetDetail;
using Sogeti.Academy.Application.Presentations.Queries.GetFile;
using Sogeti.Academy.Application.Presentations.Queries.GetList;
using Xunit;

namespace Sogeti.Academy.Api.Test.Presentations.Controllers
{
    public class PresentationsControllerTest
    {
        private static readonly Type PresentationsControllerType = typeof(PresentationsController);
        private readonly Mock<IGetListQuery> _getListQueryMock;
        private readonly Mock<IGetDetailQuery> _getDetailQueryMock;
        private readonly Mock<IGetFileQuery> _getFileQueryMock;
        private readonly Mock<IAddPresentationCommand> _addPresentationCommandMock;
        private readonly Mock<IEditPresentationCommand> _editPresentationCommandMock;
        private readonly Mock<IRemovePresentationCommand> _removePresentationCommandMock;
        private readonly PresentationsController _presentationsController;

        public PresentationsControllerTest()
        {
            _getFileQueryMock = new Mock<IGetFileQuery>();
            _getDetailQueryMock = new Mock<IGetDetailQuery>();
            _getListQueryMock = new Mock<IGetListQuery>();
            _addPresentationCommandMock = new Mock<IAddPresentationCommand>();
            _editPresentationCommandMock = new Mock<IEditPresentationCommand>();
            _removePresentationCommandMock = new Mock<IRemovePresentationCommand>();

            _presentationsController = new PresentationsController(_getListQueryMock.Object,
                _getDetailQueryMock.Object,
                _getFileQueryMock.Object,
                _addPresentationCommandMock.Object,
                _editPresentationCommandMock.Object,
                _removePresentationCommandMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldExecuteListQuery()
        {
            var expected = new ListViewModel();
            _getListQueryMock.Setup(s => s.Execute()).ReturnsAsync(expected);

            var result = (HttpOkObjectResult)await _presentationsController.GetAll();
            Assert.Same(expected, result.Value);
        }

        [Fact]
        public async Task GetFile_ShouldExecuteGetFileQuery()
        {
            var presentationId = Guid.NewGuid().ToString();
            var fileId = Guid.NewGuid().ToString();

            var viewModel = new FileDownloadViewModel
            {
                Name = "Bill",
                Type = "application/txt",
                Bytes = new byte[] {4, 5, 7, 2}
            };
            _getFileQueryMock.Setup(s => s.Execute(presentationId, fileId)).ReturnsAsync(viewModel);

            var result = (FileContentResult)await _presentationsController.GetFile(presentationId, fileId);
            Assert.Equal(viewModel.Bytes, result.FileContents);
            Assert.Equal(viewModel.Name, result.FileDownloadName);
            Assert.Equal(viewModel.Type, result.ContentType.MediaType);
        }

        [Fact]
        public async Task GetDetail_ShouldExeucteDetailQuery()
        {
            var viewModel = new PresentationDetailViewModel();

            var id = Guid.NewGuid().ToString();
            _getDetailQueryMock.Setup(s => s.Execute(id)).ReturnsAsync(viewModel);

            var result = (HttpOkObjectResult) await _presentationsController.GetDetail(id);
            Assert.Same(viewModel, result.Value);
        }

        [Fact]
        public async Task Add_ShouldExecuteAddCommand()
        {
            var viewModel = new AddPresentationViewModel();

            var expected = Guid.NewGuid().ToString();
            _addPresentationCommandMock.Setup(s => s.Execute(viewModel)).ReturnsAsync(expected);

            var result = (HttpOkObjectResult) await _presentationsController.Add(viewModel);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task Edit_ShouldExecuteEditCommand()
        {
            var viewModel = new EditPresentationViewModel();

            var result = (HttpOkResult) await _presentationsController.Edit(viewModel);
            _editPresentationCommandMock.Verify(s => s.Execute(viewModel), Times.Once());
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Remove_ShouldExecuteRemoveCommand()
        {
            var viewModel = new RemovePresentationViewModel();

            var result = (HttpOkResult) await _presentationsController.Remove(viewModel);
            Assert.Equal(200, result.StatusCode);
            _removePresentationCommandMock.Verify(s => s.Execute(viewModel), Times.Once());
        }

        [Fact]
        public void Controller_ShouldSpecifyRoute()
        {
            var route = PresentationsControllerType.GetCustomAttribute<RouteAttribute>();
            Assert.Equal("presentations", route.Template);
        }

        [Fact]
        public void GetAll_ShouldSpecifyGet()
        {
            var attribute = PresentationsControllerType.GetMethod("GetAll").GetCustomAttribute<HttpGetAttribute>();
            Assert.Equal("", attribute.Template);
        }

        [Fact]
        public void GetDetail_ShouldSpecifyGet()
        {
            var attribute = PresentationsControllerType.GetMethod("GetDetail").GetCustomAttribute<HttpGetAttribute>();
            Assert.Equal("{id}", attribute.Template);
        }

        [Fact]
        public void GetFile_ShouldSpecifyGet()
        {
            var attribute = PresentationsControllerType.GetMethod("GetFile").GetCustomAttribute<HttpGetAttribute>();
            Assert.Equal("{presentationId}/files/{fileId}", attribute.Template);
        }

        [Fact]
        public void Add_ShouldSpecifyPost()
        {
            var attribute = PresentationsControllerType.GetMethod("Add").GetCustomAttribute<HttpPostAttribute>();
            Assert.Equal("", attribute.Template);
        }

        [Fact]
        public void Edit_ShouldSpecifyPut()
        {
            var attribute = PresentationsControllerType.GetMethod("Edit").GetCustomAttribute<HttpPutAttribute>();
            Assert.Equal("{id}", attribute.Template);
        }

        [Fact]
        public void Remove_ShouldSpecifyDelete()
        {
            var attribute = PresentationsControllerType.GetMethod("Remove").GetCustomAttribute<HttpDeleteAttribute>();
            Assert.Equal("{id}", attribute.Template);
        }
    }
}
