using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMoq;
using Moq;
using Sogeti.Academy.Api.General.Http;
using Sogeti.Academy.Api.General.Results;
using Sogeti.Academy.Api.Presentations.Controllers;
using Sogeti.Academy.Api.Test.Http;
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
        private readonly AutoMoqer _autoMocker;
        private readonly PresentationsController _presentationsController;

        public PresentationsControllerTest()
        {
            _autoMocker = new AutoMoqer();

            _autoMocker.GetMock<IServer>()
                .Setup(s => s.MapPath("~/App_Data"))
                .Returns(Path.Combine(Directory.GetCurrentDirectory()));
            _presentationsController = _autoMocker.Create<PresentationsController>();
        }

        [Fact]
        public async Task GetAll_ShouldExecuteListQuery()
        {
            var expected = new ListViewModel();
            _autoMocker.GetMock<IGetListQuery>().Setup(s => s.Execute()).ReturnsAsync(expected);

            var result = (OkNegotiatedContentResult<ListViewModel>)await _presentationsController.GetAll();
            Assert.Same(expected, result.Content);
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
                Bytes = new byte[] { 45, 68, 78 }
            };
            _autoMocker.GetMock<IGetFileQuery>().Setup(s => s.Execute(presentationId, fileId)).ReturnsAsync(viewModel);

            var result = (FileResult)await _presentationsController.GetFile(presentationId, fileId);
            Assert.Same(viewModel, result.ViewModel);
        }

        [Fact]
        public async Task GetDetail_ShouldExeucteDetailQuery()
        {
            var viewModel = new PresentationDetailViewModel();

            var id = Guid.NewGuid().ToString();
            _autoMocker.GetMock<IGetDetailQuery>().Setup(s => s.Execute(id)).ReturnsAsync(viewModel);

            var result = (OkNegotiatedContentResult<PresentationDetailViewModel>)await _presentationsController.GetDetail(id);
            Assert.Same(viewModel, result.Content);
        }

        [Fact]
        public async Task Add_ShouldExecuteAddCommand()
        {
            AddPresentationViewModel viewModel = null;

            var expected = Guid.NewGuid().ToString();
            _autoMocker.GetMock<IAddPresentationCommand>()
                .Setup(s => s.Execute(It.IsAny<AddPresentationViewModel>()))
                .Callback<AddPresentationViewModel>(vm => viewModel = vm)
                .ReturnsAsync(expected);

            _presentationsController.Request = CreateAddRequest("bigTopic", "I have a description?", 5);
            var result = (OkNegotiatedContentResult<string>)await _presentationsController.Add();
            Assert.Equal(expected, result.Content);
            Assert.NotNull(viewModel);
            Assert.Equal("bigTopic", viewModel.Topic);
            Assert.Equal("I have a description?", viewModel.Description);
            AssertHasNewFiles(5, viewModel.Files);
        }

        [Fact]
        public async Task Edit_ShouldExecuteEditCommand()
        {
            EditPresentationViewModel viewModel = null;
            _autoMocker.GetMock<IEditPresentationCommand>()
                .Setup(s => s.Execute(It.IsAny<EditPresentationViewModel>()))
                .Callback<EditPresentationViewModel>(vm => viewModel = vm)
                .Returns(Task.CompletedTask);

            _presentationsController.Request = CreateUpdateRequest("something id", "no topic", "big description", 2, 3);

            var result = await _presentationsController.Edit();
            Assert.NotNull(viewModel);
            Assert.Equal("something id", viewModel.Id);
            Assert.Equal("no topic", viewModel.Topic);
            Assert.Equal("big description", viewModel.Description);
            AssertHasExistingFiles(2, viewModel.Files);
            AssertHasNewFiles(3, viewModel.Files);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Remove_ShouldExecuteRemoveCommand()
        {
            var viewModel = new RemovePresentationViewModel();

            var result = await _presentationsController.Remove(viewModel);
            Assert.IsType<OkResult>(result);
            _autoMocker.GetMock<IRemovePresentationCommand>().Verify(s => s.Execute(viewModel), Times.Once());
        }

        [Fact]
        public void Controller_ShouldSpecifyRoute()
        {
            var route = PresentationsControllerType.GetCustomAttribute<RoutePrefixAttribute>();
            Assert.Equal("presentations", route.Prefix);
        }

        [Fact]
        public void GetAll_ShouldSpecifyGet()
        {
            var attribute = PresentationsControllerType.GetMethod("GetAll").GetCustomAttribute<HttpGetAttribute>();
            var route = PresentationsControllerType.GetMethod("GetAll").GetCustomAttribute<RouteAttribute>();
            Assert.Equal("", route.Template);
            Assert.IsType<HttpGetAttribute>(attribute);
        }

        [Fact]
        public void GetDetail_ShouldSpecifyGet()
        {
            var route = PresentationsControllerType.GetMethod("GetDetail").GetCustomAttribute<RouteAttribute>();
            var attribute = PresentationsControllerType.GetMethod("GetDetail").GetCustomAttribute<HttpGetAttribute>();
            Assert.Equal("{id}", route.Template);
            Assert.NotNull(attribute);
        }

        [Fact]
        public void GetFile_ShouldSpecifyGet()
        {
            var route = PresentationsControllerType.GetMethod("GetFile").GetCustomAttribute<RouteAttribute>();
            var attribute = PresentationsControllerType.GetMethod("GetFile").GetCustomAttribute<HttpGetAttribute>();
            Assert.Equal("{presentationId}/files/{fileId}", route.Template);
            Assert.IsType<HttpGetAttribute>(attribute);
        }

        [Fact]
        public void Add_ShouldSpecifyPost()
        {
            var attribute = PresentationsControllerType.GetMethod("Add").GetCustomAttribute<HttpPostAttribute>();
            var route = PresentationsControllerType.GetMethod("Add").GetCustomAttribute<RouteAttribute>();
            Assert.Equal("", route.Template);
            Assert.IsType<HttpPostAttribute>(attribute);
        }

        [Fact]
        public void Edit_ShouldSpecifyPut()
        {
            var attribute = PresentationsControllerType.GetMethod("Edit").GetCustomAttribute<HttpPutAttribute>();
            var route = PresentationsControllerType.GetMethod("Edit").GetCustomAttribute<RouteAttribute>();
            Assert.Equal("{id}", route.Template);
            Assert.IsType<HttpPutAttribute>(attribute);
        }

        [Fact]
        public void Remove_ShouldSpecifyDelete()
        {
            var attribute = PresentationsControllerType.GetMethod("Remove").GetCustomAttribute<HttpDeleteAttribute>();
            var route = PresentationsControllerType.GetMethod("Remove").GetCustomAttribute<RouteAttribute>();
            Assert.Equal("{id}", route.Template);
            Assert.IsType<HttpDeleteAttribute>(attribute);
        }

        private static void AssertHasExistingFiles(int count, EditFileViewModel[] viewModels)
        {
            var existing = viewModels.Where(v => !string.IsNullOrEmpty(v.Id)).ToArray();
            Assert.Equal(count, existing.Length);
            for (var i = 0; i < count; i++)
            {
                Assert.Equal($"Good name {i}", existing[i].Name);
                Assert.Equal($"my old file {i}", existing[i].Id);
                Assert.Equal("plain/text", existing[i].Type);
            }
        }

        private static void AssertHasNewFiles(int count, EditFileViewModel[] editFileViewModels)
        {
            var newFiles = editFileViewModels.Where(e => string.IsNullOrEmpty(e.Id)).ToArray();
            Assert.Equal(count, newFiles.Length);
            for (var i = 0; i < count; i++)
            {
                Assert.Equal($"BigFile{i}.text", newFiles[i].Name);
                Assert.Equal($"plain/text-{i}", newFiles[i].Type);
                Assert.Equal(Enumerable.Repeat((byte) i, i).ToArray(), newFiles[i].Bytes);
            }
        }

        private static void AssertHasNewFiles(int count, AddFileViewModel[] files)
        {
            Assert.Equal(count, files.Length);
            for (var i = 0; i < count; i++)
            {
                Assert.Equal($"NewFile{i}.text", files[i].Name);
                Assert.Equal($"plain/text-{i}", files[i].Type);
                Assert.Equal(Enumerable.Repeat((byte)i, i).ToArray(), files[i].Bytes);
            }
        }

        private static HttpRequestMessage CreateUpdateRequest(string id = "", string topic = "", string description = "", int existingFileCount = 0, int newFileCount = 0)
        {
            var builder = new MultiPartFormRequestBuilder()
                .WithFormField("topic", topic)
                .WithFormField("id", id)
                .WithFormField("description", description);

            for (var i = 0; i < existingFileCount; i++)
                builder.WithFileFormField(i, $"my old file {i}", $"Good name {i}", "plain/text");

            for (var i = 0; i < newFileCount; i++)
                builder.WithFile(Enumerable.Repeat((byte) i, i).ToArray(), $"BigFile{i}.text", $"Name{i}", $"plain/text-{i}");

            return builder
                .Build(HttpMethod.Put);
        }

        private static HttpRequestMessage CreateAddRequest(string topic, string description, int fileCount)
        {
            var builder = new MultiPartFormRequestBuilder()
                .WithFormField("topic", topic)
                .WithFormField("description", description);

            for (var i = 0; i < fileCount; i++)
                builder.WithFile(Enumerable.Repeat((byte)i, i).ToArray(), $"NewFile{i}.text", $"Name{i}", $"plain/text-{i}");

            return builder.Build(HttpMethod.Post);
        }
    }
}
