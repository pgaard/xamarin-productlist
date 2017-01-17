
namespace ProductList.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;

    using Microsoft.Practices.Unity;

    using ProductList.Models;
    using ProductList.Services;

    using Xamarin.Forms;

    public class ProductSearchViewModel : BaseViewModel
    {
        private readonly IProductService productService;
        private readonly IPageService pageService;

        private ProductCollection productCollection;
        private int currentPage = 1;
        private string searchTerm;
        private string message;

        private ObservableCollection<Product> products;

        public ICommand DoSearchCommand { get; private set; }
        public ICommand SelectProductCommand { get; private set; }
        public ICommand ItemAppearingCommand { get; private set; }
        
        public ProductSearchViewModel(IProductService productService, IPageService pageService)
        {
            this.productService = productService;
            this.pageService = pageService;
            this.DoSearchCommand = new Command<string>(async text => await this.DoSearch(text));
            this.SelectProductCommand = new Command<Product>(async product => await this.SelectProduct(product));
            this.ItemAppearingCommand = new Command<Product>(async product => await this.ItemAppearing(product));
        }

        private Product selectedProduct;
        private bool isSearching;
        private int hitCount;

        public ObservableCollection<Product> Products
        {
            get { return this.products;}
            set { this.SetValue(ref this.products, value);}
        }

        public Product SelectedProduct
        {
            get { return this.selectedProduct; }
            set { this.SetValue(ref this.selectedProduct, value); }
        }

        public bool IsSearching
        {
            get{ return this.isSearching; }
            set{ this.SetValue(ref this.isSearching, value); }
        }

        public int HitCount
        {
            get { return this.hitCount; }
            set { this.SetValue(ref this.hitCount, value); }
        }

        public string Message
        {
            get{ return this.message; }
            set { this.SetValue(ref this.message, value); }
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

        public async Task SelectProduct(Product product)
        {
            if (product == null)
            {
                return;
            }

            await this.pageService.PushAsync("ProductDetail", new [] { new ParameterOverride("product", product) });
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
    }

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
}
