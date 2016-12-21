

namespace ProductList.Services
{
    using System.Threading.Tasks;
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
    }
}
