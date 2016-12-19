using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Services
{
    using System.Net;
    using System.Net.Http;

    public interface IClientService
    {
        CookieCollection Cookies { get; }

        Task<bool> GetToken(string userName, string password);

        Task<HttpResponseMessage> GetAsync(string path);

        Task<HttpResponseMessage> PostAsync(string path, HttpContent content);

        void StoreState();

        void LoadState();
    }
}
