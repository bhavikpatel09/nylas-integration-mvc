using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace NylasMVCApp.Web
{
    public static class APIService
    {
        public static async Task<HttpResponseMessage> Get(string url, string accessToken, string queryString = "")
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.ServiceConstants.Uri);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    HttpResponseMessage response = await client.GetAsync(url + queryString);
                    return response;
                }
            }
            return null;
        }
        public static async Task<HttpResponseMessage> Post(string url, string accessToken, StringContent content)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.ServiceConstants.Uri);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    //FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    return response;
                }
            }
            return null;
        }
    }
}