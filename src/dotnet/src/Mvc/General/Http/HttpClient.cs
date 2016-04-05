using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sogeti.Academy.Mvc.General.Http
{
    public interface IHttpClient : IDisposable
    {
        Task<T> GetJson<T>(string url);
        Task PostJson<T>(string url, T model);
        Task<TResult> PostJson<TModel, TResult>(string url, TModel model);
        Task PutJson<T>(string url, T model);
        Task DeleteJson(string url);
    }

    public class HttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _innerClient;
        private readonly JsonSerializerSettings _serializerSettings;
        
        public HttpClient()
        {
            _innerClient = new System.Net.Http.HttpClient();
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
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

        public Task PostJson<T>(string url, T model)
        {
            var content = CreateHttpContent(model);
            return _innerClient.PostAsync(url, content);
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
        
        private HttpContent CreateHttpContent<T>(T model)
        {
            var json = JsonConvert.SerializeObject(model, _serializerSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}