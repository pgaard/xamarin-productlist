namespace ProductList
{
    using System;

    using Xamarin.Forms;
    using ProductList.Models;

    public partial class ProductDetail : ContentPage
    {
        public ProductDetail(Product product)
        {
            this.BindingContext = product;
            InitializeComponent();
        }

        private void Handle_AddToCart(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
