
namespace ProductList.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;

    using Newtonsoft.Json;

    using ProductList.Models;

    public class AccountService : IAccountService
    {
        private const string Host = "search.insitesoftqa.com";
        private const string TokenUri = "/identity/connect/token";
        private const string SessionsUri = "/api/v1/sessions";
        private const string ClientId = "isc";
        private const string ClientSecret = "009AC476-B28E-4E33-8BAE-B5F103A142BC";

        private readonly HttpClient client;

        public AccountService()
        {
            this.client = new HttpClient();
        }

        public async Task<bool> Authenticate(string userName, string password)
        {
            try
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
                
                this.client.DefaultRequestHeaders.Add(
                    "Authorization",
                    "Basic " + this.Base64Encode(ClientId + ":" + ClientSecret));

                var tokenResult = await this.client.PostAsync($"http://{Host}/{TokenUri}", content);
                if (tokenResult.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                var resultContent = tokenResult.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<TokenResult>(resultContent);

                content =
                    new FormUrlEncodedContent(
                        new[]
                        {
                            new KeyValuePair<string, string>("username", userName),
                            new KeyValuePair<string, string>("password", password)
                        });

                this.client.DefaultRequestHeaders.Remove("Authorization");
                this.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);
                var sessionResult = await this.client.PostAsync($"http://{Host}/{SessionsUri}", content);

                if (sessionResult.StatusCode != HttpStatusCode.Created)
                {
                    return false;
                }

                resultContent = sessionResult.Content.ReadAsStringAsync().Result;
                var session = JsonConvert.DeserializeObject<Session>(resultContent);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

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
