using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sogeti.Academy.Presentation.General.Http
{
    public interface IHttpClient : IDisposable
    {
        Task<T> GetJson<T>(string url);
        Task<TResult> PostJson<TModel, TResult>(string url, TModel model);
        Task PutJson<T>(string url, T model);
        Task DeleteJson(string url);
    }

    public class HttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _innerClient;
        
        public HttpClient()
        {
            _innerClient = new System.Net.Http.HttpClient();
        }
        
        public Task DeleteJson(string url)
        {
            return _innerClient.DeleteAsync(url);
        }

        public void Dispose()
        {
            _innerClient.Dispose();
        }

        public async Task<T> GetJson<T>(string url)
        {
            var json = await _innerClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<TResult> PostJson<TModel, TResult>(string url, TModel model)
        {
            var content = CreateHttpContent(model);
            var httpResponse = await _innerClient.PostAsync(url, content);
            var json = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(json);
        }

        public Task PutJson<T>(string url, T model)
        {
            var content = CreateHttpContent(model);
            return _innerClient.PutAsync(url, content);
        }
        
        private static HttpContent CreateHttpContent<T>(T model)
        {
            var json = JsonConvert.SerializeObject(model);
            return new StringContent(json);
        }
    }
}