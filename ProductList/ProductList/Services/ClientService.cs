namespace ProductList.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;

    using ModernHttpClient;

    using Newtonsoft.Json;

    using Xamarin.Forms;

    public class ClientService : IClientService
    {

        private const string Protocol = "http://";

        private const string Host = "search.insitesoftqa.com";
        //private const string Host = "10.0.2.2"; 
        private const string TokenUri = "/identity/connect/token";
        private const string ClientId = "isc";
        private const string ClientSecret = "009AC476-B28E-4E33-8BAE-B5F103A142BC";

        private readonly HttpClient client;
        private readonly HttpClientHandler httpClientHandler;

        private string bearerToken;
       
        public ClientService()
        {
            this.httpClientHandler = new NativeMessageHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer(),
                ClientCertificateOptions = ClientCertificateOption.Automatic               
            };
            
            this.client = new HttpClient(this.httpClientHandler);

            this.LoadState();
        }

        public CookieCollection Cookies => this.httpClientHandler.CookieContainer.GetCookies(new Uri($"{Protocol}{Host}"));

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

            var tokenResult = await this.client.PostAsync($"{Protocol}{Host}/{TokenUri}", content);
            if (tokenResult.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            var resultContent = tokenResult.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<TokenResult>(resultContent);
            this.bearerToken = token.access_token;

            this.SetBearerTokenHeader();

            this.StoreState();
            return true;
        }

        private string MakeUrl(string path)
        {
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }
            return $"{Protocol}{Host}/{path}";
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {                        
            return await this.client.GetAsync(this.MakeUrl(path));
        }

        public async Task<HttpResponseMessage> PostAsync(string path, HttpContent content)
        {
            return await this.client.PostAsync(this.MakeUrl(path), content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            return await this.client.DeleteAsync(this.MakeUrl(path));
        }

        public async Task<HttpResponseMessage> PatchAsync(string path, HttpContent content)
        {
            return await this.client.PatchAsync(new Uri(this.MakeUrl(path)), content);
        }

        public async Task<HttpResponseMessage> PutAsync(string path, HttpContent content)
        {
            return await this.client.PutAsync(new Uri(this.MakeUrl(path)), content);
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public void StoreState()
        {            
            foreach (Cookie cookie in this.Cookies)
            {
                Application.Current.Properties["cookies_" + cookie.Name] = cookie.Value;
            }
            Application.Current.Properties["bearerToken"] = this.bearerToken;
        }

        public void LoadState()
        {
            foreach (var prop in Application.Current.Properties)
            {
                if (prop.Key.StartsWith("cookies_"))
                {
                    var key = prop.Key.Substring("cookie_".Length+1);
                    this.httpClientHandler.CookieContainer.Add(new Uri($"{Protocol}{Host}"),new Cookie(key, prop.Value.ToString()));
                }
            }

            if (Application.Current.Properties.ContainsKey("bearerToken"))
            {                
                this.bearerToken = Application.Current.Properties["bearerToken"]?.ToString();
                this.SetBearerTokenHeader();
            }
        }

        private void SetBearerTokenHeader()
        {
            if (this.bearerToken != null)
            {
                if (this.client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    this.client.DefaultRequestHeaders.Remove("Authorization");
                }
                this.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.bearerToken);
            }
        }
    }

    public class TokenResult
    {
        public string access_token { get; set; }
    }

    // http://stackoverflow.com/questions/26218764/patch-async-requests-with-windows-web-http-httpclient-class
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent iContent)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };

            var response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {     
            }

            return response;
        }
    }
}
 