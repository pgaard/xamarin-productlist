namespace ProductList.ViewModels
{
    using System.Threading.Tasks;
    using ProductList.Services;
    using Microsoft.Practices.Unity;
    using ProductList.Models;

    public class CartPageViewModel : BaseViewModel
    {
        private ICartService cartService;
        private Cart cart;
        private int lineCount;

        public CartPageViewModel()
        {
            this.cartService = App.Container.Resolve<ICartService>();            
            Task.Factory.StartNew(this.LoadCart);
        }

        private async Task LoadCart()
        {
            this.Cart = await this.cartService.GetCart();
            this.LineCount = this.Cart.cartLines.Count;
        }

        public Cart Cart
        {
            get { return this.cart; }
            set { this.SetValue(ref this.cart, value); }
        }

        public int LineCount
        {
            get { return this.lineCount; }
            set { this.SetValue(ref this.lineCount, value);}
        }
    }
}
