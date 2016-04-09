using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Sogeti.Academy.Application.Presentations.Queries.GetFile;

namespace Sogeti.Academy.Api.General.Results
{
    public class FileResult : IHttpActionResult
    {
        private readonly FileDownloadViewModel _viewModel;

        public FileDownloadViewModel ViewModel => _viewModel;

        public FileResult(FileDownloadViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(_viewModel.Bytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(_viewModel.Type);
            response.Content.Headers.ContentLength = _viewModel.Bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = _viewModel.Name,
                Size = _viewModel.Bytes.LongLength
            };
            return Task.FromResult(response);
        }
    }
}
