using Xamarin.Forms;

namespace ProductList
{
    using ProductList.ViewModels;

    public partial class CartPage : ContentPage
    {
        public CartPage()
        {            
            this.ViewModel = new CartPageViewModel();
            InitializeComponent();            
        }

        public CartPageViewModel ViewModel
        {
            get { return BindingContext as CartPageViewModel; }
            set { BindingContext = value; }
        }
    }
}
