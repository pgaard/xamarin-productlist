using Xamarin.Forms;

namespace ProductList
{
    using System;

    using Microsoft.Practices.Unity;

    using ProductList.Models;
    using ProductList.ViewModels;

    public partial class CartPage : ContentPage
    {
        public CartPage()
        {                        
            InitializeComponent();
            this.ViewModel = App.Container.Resolve<CartPageViewModel>();
        }
        protected override async void OnAppearing()
        {
            await this.ViewModel.LoadCart();
            base.OnAppearing();
        }

        public CartPageViewModel ViewModel
        {
            get { return BindingContext as CartPageViewModel; }
            set { BindingContext = value; }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
        }

        private void QuantityEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void QuantityEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            var id = ((sender as BindableObject).BindingContext as CartLine).id;
            this.ViewModel.UpdateCartLineCommand.Execute(id);
        }

        private void CheckOut_Clicked(object sender, EventArgs e)
        {
            
        }        
    }
}
