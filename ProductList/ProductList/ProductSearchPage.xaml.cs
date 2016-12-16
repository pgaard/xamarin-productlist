﻿namespace ProductList
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductList.Models;

    using Xamarin.Forms;
    using ProductList.Services;
    using ProductList.ViewModels;

    public partial class ProductSearchPage : ContentPage
    {        
        public ProductSearchPage()
        {
            this.ViewModel = new ProductSearchViewModel();            
            this.InitializeComponent();

            //var test = ImageSource.FromResource("ProductList.Images.ShoppingCart-48.png");
            //var test =ImageSource.FromFile("shopping.png");
            //this.cartToolBar.Icon = (FileImageSource)ImageSource.FromFile("shopping.png");            
            //this.testImage.Source = test;
            
            //var assembly = typeof(ProductSearchPage).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);
        }

        private async void Handle_Search(object sender, EventArgs e)
        {
            await Task.Run(() => this.ViewModel.DoSearchCommand.Execute((sender as SearchBar).Text));

            // scroll to the top
            var products = (this.ListView.ItemsSource as ObservableCollection<Product>);
            if (products != null && products.Any())
            {
                this.ListView.ScrollTo(products.First(), ScrollToPosition.Start, false);
            }
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.ViewModel.SelectProductCommand.Execute(e.SelectedItem);
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            this.ViewModel.ItemAppearingCommand.Execute(e.Item);
        }

        public ProductSearchViewModel ViewModel
        {
            get { return BindingContext as ProductSearchViewModel; }
            set { BindingContext = value; }
        }

        private void ToolbarItem_Cart(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new CartPage());          
        }

        private void ToolbarItem_Account(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new SignInPage());
        }
    }
}
