
namespace ProductList.Services
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;

    using Microsoft.Practices.Unity;

    using Newtonsoft.Json;

    using ProductList.Models;

    public class AccountService : IAccountService
    {
        private const string SessionsPath = "api/v1/sessions";
        private const string IsAuthenticatedPath = "account/isauthenticated";
        private readonly IClientService client;          
        private Session session;

        public AccountService()
        {
            this.client = App.Container.Resolve<IClientService>();
        }

        public async Task<bool> Authenticate(string userName, string password)
        {
            try
            {
                if (!await this.client.GetToken(userName, password))
                {
                    return false;
                }

                var content =
                    new FormUrlEncodedContent(
                        new[]
                        {
                            new KeyValuePair<string, string>("username", userName),
                            new KeyValuePair<string, string>("password", password)
                        });
              
                var sessionResult = await this.client.PostAsync(SessionsPath, content);
                if (sessionResult.StatusCode != HttpStatusCode.Created)
                {
                    return false;
                }

                var resultContent = sessionResult.Content.ReadAsStringAsync().Result;
                this.session = JsonConvert.DeserializeObject<Session>(resultContent);
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> IsAuthenticated()
        {
            try
            {
                var result = await this.client.GetAsync(IsAuthenticatedPath);
                var resultContent = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IsAuthenticatedResult>(resultContent).isAuthenticatedOnServer;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class IsAuthenticatedResult
    {
        public bool isAuthenticatedOnServer;
    }
}
