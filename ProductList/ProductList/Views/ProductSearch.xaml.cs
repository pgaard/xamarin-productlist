namespace ProductList
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductList.Models;

    using Xamarin.Forms;
    using ProductList.Services;
    using ProductList.ViewModels;

    public partial class ProductSearch : ContentPage
    {        
        public ProductSearch()
        {
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

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.ViewModel.SelectProductCommand.Execute(e.SelectedItem as Product);
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            this.ViewModel.ItemAppearingCommand.Execute(e.Item as Product);
        }

        public ProductSearchViewModel ViewModel => this.BindingContext as ProductSearchViewModel;
    }
}
