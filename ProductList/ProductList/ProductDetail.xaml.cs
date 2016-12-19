namespace ProductList
{
    using System;

    using Xamarin.Forms;
    using ProductList.Models;
    using ProductList.ViewModels;

    public partial class ProductDetail : ContentPage
    {
        public ProductDetailViewModel ViewModel
        {
            get { return BindingContext as ProductDetailViewModel; }
            set { BindingContext = value; }
        }

        public ProductDetail(Product product)
        {
            this.ViewModel = new ProductDetailViewModel(product);
            InitializeComponent();
        }

        private void Handle_AddToCart(object sender, EventArgs e)
        {
            this.ViewModel.AddToCartCommand.Execute(this.ViewModel.Product);
        }
    }
}
