namespace ProductList.ViewModels
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using ProductList.Services;
    using Microsoft.Practices.Unity;
    using ProductList.Models;

    using Xamarin.Forms;

    public class CartPageViewModel : BaseViewModel
    {
        private ICartService cartService;
        private Cart cart;
        private int lineCount;
        private bool isLoading;

        public ICommand DeleteCartLineCommand { get; private set; }
        public ICommand UpdateCartLineCommand { get; private set; }

        public CartPageViewModel()
        {
            this.cartService = App.Container.Resolve<ICartService>();            
            Task.Factory.StartNew(this.LoadCart);
            this.DeleteCartLineCommand = new Command<string>(async id => await this.DeleteCartLine(id));
            this.UpdateCartLineCommand = new Command<string>(async id => await this.UpdateCartLine(id));
        }

        private void DeleteCartLine(object s)
        {
            
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

        private async Task LoadCart()
        {
            this.IsLoading = true;
            this.Cart = await this.cartService.GetCart();
            this.LineCount = this.Cart.cartLines.Count;
            this.IsLoading = false;
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

        public bool IsLoading
        {
            get { return this.isLoading; }
            set { this.SetValue(ref this.isLoading, value); }
        }
    }
}
