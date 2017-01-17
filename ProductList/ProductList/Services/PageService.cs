

namespace ProductList.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Practices.Unity;

    using Xamarin.Forms;

    public class PageService : IPageService
    {
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel = null)
        {
            if (string.IsNullOrEmpty(cancel))
            { 
                await Application.Current.MainPage.DisplayAlert(title, message, ok);
                return true;
            }
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task PushAsync(string pageName, ParameterOverride[] parameters = null)
        {
            Page page;
            if (parameters != null)
            {
                var resolverOverride = parameters.Select(p => p as ResolverOverride).ToArray();
                page = App.Container.Resolve<Page>(pageName, resolverOverride);
            }
            else
            {
                page = App.Container.Resolve<Page>(pageName);
            }
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
