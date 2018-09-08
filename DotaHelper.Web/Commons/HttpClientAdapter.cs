using DotaHelper.Services.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotaHelper.Web.Commons
{
    public class HttpClientAdapter : IHttpClient
    {
        private readonly HttpClient client = new HttpClient();

        public void Dispose()
        {
            this.client.Dispose();
        }

        public async Task<string> GetAsync(string uri, IDictionary<string, string> headers = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            if (headers != null)
            {
                foreach (var pair in headers)
                {
                    request.Headers.Add(pair.Key, pair.Value);
                }
            }

            var response = await this.client.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
