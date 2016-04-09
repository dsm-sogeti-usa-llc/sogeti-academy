using System.Net.Http;
using System.Net.Http.Headers;

namespace Sogeti.Academy.Api.Test.Http
{
    public class MultiPartFormRequestBuilder
    {
        private readonly MultipartFormDataContent _multipartFormData;

        public MultiPartFormRequestBuilder()
        {
            _multipartFormData = new MultipartFormDataContent();
            _multipartFormData.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        }

        public MultiPartFormRequestBuilder WithFormField(string key, string value)
        {
            _multipartFormData.Add(new StringContent(value), key);
            return this;
        }

        public MultiPartFormRequestBuilder WithFileFormField(int index, string id, string name, string type)
        {
            return WithFormField($"files[{index}][id]", id)
                .WithFormField($"files[{index}][name]", name)
                .WithFormField($"files[{index}][type]", type);
        }

        public MultiPartFormRequestBuilder WithFile(byte[] bytes, string fileName, string name = "", string contentType = "")
        {
            var content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName,
                Name = name
            };
            _multipartFormData.Add(content);
            return this;

        }

        public HttpRequestMessage Build(HttpMethod method)
        {
            return new HttpRequestMessage
            {
                Content = _multipartFormData,
                Method = method
            };
        }
    }
}
