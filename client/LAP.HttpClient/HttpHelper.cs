using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LAP.HttpClient
{
    public static class HttpHelper
    {
        private static async Task<string> GetAsync(string url)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, StatusCode: {response.StatusCode}");
            }
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> PostAsync(string url, string contentJson)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(contentJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error,StatusCode: {response.StatusCode}");
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            return responseJson;
        }
    }
}
