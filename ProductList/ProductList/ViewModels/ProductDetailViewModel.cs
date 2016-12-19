namespace ProductList.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Microsoft.Practices.Unity;

    using ProductList.Models;
    using ProductList.Services;

    using Xamarin.Forms;

    public class ProductDetailViewModel : BaseViewModel
    {
        private readonly ICartService cartService;

        private Product product;
        private string message;

        public ICommand AddToCartCommand { get; private set; }

        public ProductDetailViewModel(Product product)
        {
            this.product = product;
            this.cartService = App.Container.Resolve<ICartService>();
            this.AddToCartCommand = new Command<Product>(async p => await this.AddToCart(p));
        }

        private async Task AddToCart(Product product)
        {
            var result = await this.cartService.AddToCart(product);
            this.Message = result ? "Added to cart" : "Failed to add to cart";
        }

        public Product Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.SetValue(ref this.product, value);
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.SetValue(ref this.message, value);
            }
        }
    }
}
