﻿namespace ProductList
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Practices.Unity;

    using ProductList.Models;

    using Xamarin.Forms;
    using ProductList.Services;
    using ProductList.ViewModels;

    public partial class ProductSearchPage : ContentPage
    {        
        public ProductSearchPage()
        {
            this.ViewModel = App.Container.Resolve<ProductSearchViewModel>();            
            this.InitializeComponent();            
            //var test = ImageSource.FromResource("ProductList.Images.ShoppingCart-48.png");
            //var test =ImageSource.FromFile("shopping.png");
            //this.cartToolBar.Icon = (FileImageSource)ImageSource.FromFile("shopping.png");            
            //this.testImage.Source = test;
            
            //var assembly = typeof(ProductSearchPage).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);            
            
        }

        protected override void OnAppearing()
        {
            this.SearchText.Text = (Application.Current as App).Search;
            base.OnAppearing();
        }

        private async void Handle_Search(object sender, EventArgs e)
        {
            (Application.Current as App).Search = (sender as SearchBar).Text;
            await Task.Run(() => this.ViewModel.DoSearchCommand.Execute((sender as SearchBar).Text));

            // scroll to the top
            var products = (this.ListView.ItemsSource as ObservableCollection<Product>);
            if (products != null && products.Any())
            {
                this.ListView.ScrollTo(products.First(), ScrollToPosition.Start, false);
            }
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            this.ViewModel.SelectProductCommand.Execute(e.Item);
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
    }
}
