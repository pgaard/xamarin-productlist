namespace ProductList.ViewModels
{
    using System.Linq;
    using System.Threading.Tasks;

    using Prism.Commands;

    using ProductList.Services;

    using Prism.Mvvm;
    using Prism.Navigation;

    using ProductList.Models;

    public class CartPageViewModel : BindableBase, INavigationAware
    {
        private readonly ICartService cartService;
        private Cart cart;
        private int lineCount;
        private bool isLoading;

        public DelegateCommand<string> DeleteCartLineCommand { get; private set; }
        public DelegateCommand<string> UpdateCartLineCommand { get; private set; }

        public CartPageViewModel(ICartService cartService)
        {
            this.cartService = cartService;
            this.DeleteCartLineCommand = new DelegateCommand<string>(async id => await this.DeleteCartLine(id));
            this.UpdateCartLineCommand = new DelegateCommand<string>(async id => await this.UpdateCartLine(id));
        }

        private async Task DeleteCartLine(string cartLineId)
        {
            // TODO confirmation popup
            await this.cartService.DeleteFromCart(cartLineId);
            await this.LoadCart();
        }

        private async Task UpdateCartLine(string cartLineId)
        {
            var cartline = this.Cart.cartLines.FirstOrDefault(c => c.id == cartLineId);
            if (cartline != null)
            {
                await this.cartService.PatchCartline(cartline);
                await this.LoadCart();
            }
        }

        public async Task LoadCart()
        {
            this.IsLoading = true;
            this.Cart = await this.cartService.GetCart();
            this.LineCount = this.Cart.cartLines.Count;
            this.IsLoading = false;
        }

        public Cart Cart
        {
            get { return this.cart; }
            set { this.SetProperty(ref this.cart, value); }
        }

        public int LineCount
        {
            get { return this.lineCount; }
            set { this.SetProperty(ref this.lineCount, value);}
        }

        public bool IsLoading
        {
            get { return this.isLoading; }
            set { this.SetProperty(ref this.isLoading, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await this.LoadCart();
        }
    }
}
