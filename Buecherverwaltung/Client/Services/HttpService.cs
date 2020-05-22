using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Buecherverwaltung.Client.Services
{
    public class HttpService
    {
        private readonly HttpClient _client;
        private const string MediaType = "application/json";

        private HttpClient GetHttpClient()
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            return _client;
        }

        public HttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> Get(string url)
        {
            var client = GetHttpClient();
            var responce = await client.GetAsync(url);
            return await responce.Content.ReadAsStringAsync();
        }

        public async Task<string> Get(string url, string parameters)
        {
            var client = GetHttpClient();
            var responce = await client.GetAsync($"{url}/{parameters}");
            return await responce.Content.ReadAsStringAsync();
        }

        public async Task<string> Delete<T>(T id, string url)
        {
            var client = GetHttpClient();
            var respone = await client.DeleteAsync($"{url}/{id}");
            return await respone.Content.ReadAsStringAsync();
        }

        public async Task<string> Post<T>(T @object, string url)
        {
            var client = GetHttpClient();
            var json = JsonConvert.SerializeObject(@object);
            var respone = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, MediaType));
            return await respone.Content.ReadAsStringAsync();
        }

        public async Task<string> Put<T>(T @object, string url)
        {
            var client = GetHttpClient();
            var json = JsonConvert.SerializeObject(@object);
            var respone = await client.PutAsync(url, new StringContent(json, Encoding.UTF8, MediaType));
            return await respone.Content.ReadAsStringAsync();
        }
    }
}
