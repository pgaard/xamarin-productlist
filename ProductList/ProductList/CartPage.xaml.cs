using Xamarin.Forms;

namespace ProductList
{
    using System;

    using ProductList.ViewModels;

    public partial class CartPage : ContentPage
    {
        public CartPage()
        {                        
            InitializeComponent();
            this.ViewModel = new CartPageViewModel();
        }

        public CartPageViewModel ViewModel
        {
            get { return BindingContext as CartPageViewModel; }
            set { BindingContext = value; }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
        }
    }
}
