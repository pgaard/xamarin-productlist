using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProductList
{
    using ProductList.Services;

    public partial class TabbedHomePage : TabbedPage
    {
        public TabbedHomePage()
        {
            this.InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.Title = "Insite";
            this.Children.Add(new SignInPage());
            this.Children.Add(new ProductSearchPage());
            this.Children.Add(new CartPage { Icon = "shopping.png", Title = "" });
        }
    }
}
