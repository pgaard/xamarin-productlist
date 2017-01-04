
namespace ProductList.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    using ProductList.Models;
    using ProductList.Services;

    public class ProductSearchViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly IProductService productService;

        private ProductCollection productCollection;
        private int currentPage = 1;
        private string searchTerm;
        private string message;

        private ObservableCollection<Product> products;

        public DelegateCommand<string> DoSearchCommand { get; private set; }
        public DelegateCommand<Product> SelectProductCommand { get; private set; }
        public DelegateCommand<Product> ItemAppearingCommand { get; private set; }
        
        public ProductSearchViewModel(INavigationService navigationService, IProductService productService)
        {
            this.navigationService = navigationService;
            this.productService = productService;
            this.DoSearchCommand = new DelegateCommand<string>(async text => await this.DoSearch(text));
            this.SelectProductCommand = new DelegateCommand<Product>(async product => await this.SelectProduct(product));
            this.ItemAppearingCommand = new DelegateCommand<Product>(async product => await this.ItemAppearing(product));
        }

        private Product selectedProduct;
        private bool isSearching;
        private int hitCount;

        public ObservableCollection<Product> Products
        {
            get { return this.products;}
            set { this.SetProperty(ref this.products, value);}
        }

        public Product SelectedProduct
        {
            get { return this.selectedProduct; }
            set { this.SetProperty(ref this.selectedProduct, value); }
        }

        public bool IsSearching
        {
            get{ return this.isSearching; }
            set{ this.SetProperty(ref this.isSearching, value); }
        }

        public int HitCount
        {
            get { return this.hitCount; }
            set { this.SetProperty(ref this.hitCount, value); }
        }

        public string Message
        {
            get{ return this.message; }
            set { this.SetProperty(ref this.message, value); }
        }

        public async Task DoSearch(string text)
        {
            this.currentPage = 1;
            await this.LoadProducts(this.currentPage, text);
            this.HitCount = this.productCollection?.pagination?.totalItemCount ?? 0;
        }

        private async Task LoadProducts(int page, string text)
        {
            this.IsSearching = true;

            if (page == 1)
            {
                this.searchTerm = text;
                this.productCollection = await this.productService.DoProductSearch(this.searchTerm, page);
            }
            else
            {
                var newProductCollection = await this.productService.DoProductSearch(this.searchTerm, page);
                if (newProductCollection != null)
                {
                    this.productCollection.products.AddRange(newProductCollection.products);
                }                
            }

            if (this.productCollection?.products != null)
            {
                this.Products = new ObservableCollection<Product>(this.productCollection.products);
            }
            else
            {
                this.Message = "Search error";
            }

            this.IsSearching = false;
        }

        private async Task SelectProduct(Product product)
        {
            if (product == null)
            {
                return;
            }

            var parameters = new NavigationParameters { { "product", product } };
            await this.navigationService.NavigateAsync("ProductDetail", parameters);
        }

        // this doesn't work right in uwp - keeps retrigging and loading. uwp returns all items as visible.
        private async Task ItemAppearing(Product product)
        {
            if (!this.IsSearching && this.Products != null && product == this.Products[Math.Max(this.Products.Count - 1, 0)])
            {
                if (this.currentPage < this.productCollection.pagination.numberOfPages)
                {
                    this.currentPage++;
                    await this.LoadProducts(this.currentPage, this.searchTerm);
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
    /*
    public class ScrollBehavior : Behavior<ListView>
    {        
        protected override void OnAttachedTo(ListView listView)
        {
            listView.ItemAppearing += (sender, args) => 
            {
                if (args.Item == (listView.ItemsSource as ObservableCollection<Product>).First())
                {
                    listView.ScrollTo(args.Item, ScrollToPosition.Start, false);
                }
            };
            
            base.OnAttachedTo(listView);
        }

        private void Bindable_ChildAdded(object sender, ElementEventArgs e)
        {            
            
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
        }   
    }
    */
}
