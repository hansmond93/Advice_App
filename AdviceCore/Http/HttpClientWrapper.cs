using AdviceCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdviceCore.Http
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public HttpClientWrapper(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; set; }


        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var response = await Client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> PostAsync<T, Tdata>(string uri, Tdata data)
        {
            try
            {
                var serialized = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await Client.PostAsync(uri, serialized);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
