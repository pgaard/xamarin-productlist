namespace ProductList.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Prism.Services;

    using ProductList.Models;
    using ProductList.Services;    

    public class ProductDetailViewModel : BindableBase, INavigationAware
    {
        private readonly ICartService cartService;

        private Product product;
        private string message;

        private IPageDialogService pageDialogService;

        public DelegateCommand<Product> AddToCartCommand { get; private set; }

        public ProductDetailViewModel(ICartService cartService, IPageDialogService pageDialogService)
        {
            this.cartService = cartService;
            this.pageDialogService = pageDialogService;
            this.AddToCartCommand = new DelegateCommand<Product>(async p => await this.AddToCart(p));
        }

        private async Task AddToCart(Product product)
        {
            var result = await this.cartService.AddToCart(product);

            await this.pageDialogService.DisplayAlertAsync("", result ? "Added to cart" : "Failed to add to cart", "ok");
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
                this.SetProperty(ref this.product, value);
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
                this.SetProperty(ref this.message, value);
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {            
            this.Product = parameters["product"] as Product;
        }
    }
}
