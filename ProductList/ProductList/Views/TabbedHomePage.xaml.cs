using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProductList
{
    using Prism.Navigation;
    public partial class TabbedHomePage : TabbedPage, INavigationAware
    {
        public TabbedHomePage()
        {
            this.InitializeComponent();
            /*
            NavigationPage.SetHasNavigationBar(this, false);
            this.Title = "Insite";
            this.Children.Add(new SignInPage());
            this.Children.Add(new ProductSearch());
            this.Children.Add(new CartPage { Icon = "shopping.png", Title = "Cart" });
            */
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
