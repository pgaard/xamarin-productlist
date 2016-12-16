using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductList.Services
{
    using System.Net;
    using System.Net.Http;

    using Newtonsoft.Json;

    public class ClientService : IClientService
    {
        private const string Host = "search.insitesoftqa.com";
        private const string TokenUri = "/identity/connect/token";
        private const string ClientId = "isc";
        private const string ClientSecret = "009AC476-B28E-4E33-8BAE-B5F103A142BC";

        private readonly HttpClient client;
        private readonly HttpClientHandler httpClientHandler;

        private string bearerToken;
       
        public ClientService()
        {
            this.httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            this.client = new HttpClient(this.httpClientHandler);
        }

        public CookieCollection Cookies => this.httpClientHandler.CookieContainer.GetCookies(new Uri($"http://{Host}"));

        public async Task<bool> GetToken(string userName, string password)
        {
            var content =
                   new FormUrlEncodedContent(
                       new[]
                       {
                            new KeyValuePair<string, string>("grant_type", "password"),
                            new KeyValuePair<string, string>("username", userName),
                            new KeyValuePair<string, string>("password", password),
                            new KeyValuePair<string, string>("scope", "iscapi offline_access")
                       });

            if (this.client.DefaultRequestHeaders.Contains("Authorization"))
            {
                this.client.DefaultRequestHeaders.Remove("Authorization");
            }
            this.client.DefaultRequestHeaders.Add("Authorization", "Basic " + this.Base64Encode(ClientId + ":" + ClientSecret));

            var tokenResult = await this.client.PostAsync($"http://{Host}/{TokenUri}", content);
            if (tokenResult.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            var resultContent = tokenResult.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<TokenResult>(resultContent);
            this.bearerToken = token.access_token;

            this.client.DefaultRequestHeaders.Remove("Authorization");
            this.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);

            return true;
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }
            return await this.client.GetAsync($"http://{Host}/{path}");
        }

        public async Task<HttpResponseMessage> PostAsync(string path, HttpContent content)
        {
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }
            return await this.client.PostAsync($"http://{Host}/{path}", content);
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

    public class TokenResult
    {
        public string access_token { get; set; }
    }
}
