using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace webApi
{
    public class WebClientHandler
    {
        private readonly IConfiguration _configuration;

        public WebClientHandler(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task POSTAsync<TReq>(string action, TReq req = null)
          where TReq : class, new()
        {

            using (var httpClient = new HttpClient())
            {
                string body = req != null ? JsonSerializer.Serialize(req) : "";

                HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

                var httpResponseMessage = await httpClient.PostAsync(new Uri(($"{_configuration.GetValue<string>("Exercise:BaseUrl")}/{action}")), httpContent);
                string resp = await httpResponseMessage.Content.ReadAsStringAsync();

            }

        }

        public async Task<TRes> POSTAsync<TReq, TRes>(string action, TReq req = null)
            where TReq : class, new()
            where TRes : class, new()
        {

            using (var httpClient = new HttpClient())
            {
                string body = req != null ? JsonSerializer.Serialize(req) : "";

                HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

                var httpResponseMessage = await httpClient.PostAsync(new Uri(($"{_configuration.GetValue<string>("Exercise:BaseUrl")}/{action}")), httpContent);
                string resp = await httpResponseMessage.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TRes>(resp);

            }

        }

        public async Task<TRes> GetAsync<TRes>(string uri)
           where TRes : class, new()
        {

            using (var httpClient = new HttpClient())
            {

                var httpResponseMessage = await httpClient.GetAsync(new Uri(($"{_configuration.GetValue<string>("Exercise:BaseUrl")}/{uri}")));

                return await JsonSerializer.DeserializeAsync<TRes>(await httpResponseMessage.Content.ReadAsStreamAsync());

            }

        }
    }
}
