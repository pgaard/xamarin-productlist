using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Services
{
    using Microsoft.Practices.Unity;

    using Xamarin.Forms;

    public interface IPageService
    {
        Task PushAsync(Page page);
        Task PushAsync(string pageName, ParameterOverride[] parameters = null);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel = null);
    }
}
