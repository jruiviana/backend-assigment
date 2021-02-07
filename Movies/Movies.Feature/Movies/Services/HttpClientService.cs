using Movies.Core.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Feature.Services
{
    public class HttpClientService : IHttpHandler, IDisposable
    {
        private HttpClient _client = new HttpClient();

        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }

        public HttpResponseMessage Post(string url, HttpContent content)
        {
            return PostAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
