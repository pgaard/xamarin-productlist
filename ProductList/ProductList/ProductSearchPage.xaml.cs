namespace ProductList
{
    using System;

    using Xamarin.Forms;
    using ProductList.Services;
    using ProductList.ViewModels;

    public partial class ProductSearchPage : ContentPage
    {        
        public ProductSearchPage()
        {
            this.ViewModel = new ProductSearchViewModel(new ProductService(), new PageService());            
            this.InitializeComponent();

            //var test = ImageSource.FromResource("ProductList.Images.ShoppingCart-48.png");
            //var test =ImageSource.FromFile("shopping.png");
            //this.cartToolBar.Icon = (FileImageSource)ImageSource.FromFile("shopping.png");            
            //this.testImage.Source = test;

            //var assembly = typeof(ProductSearchPage).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);
        }

        private void Handle_Search(object sender, EventArgs e)
        {            
            this.ViewModel.DoSearchCommand.Execute(this.SearchBar.Text);
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

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new CartPage());          
        }        
    }
}
