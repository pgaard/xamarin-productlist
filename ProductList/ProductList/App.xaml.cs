namespace ProductList
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Practices.Unity;

    using Prism.Unity;

    using ProductList.Services;
    using ProductList.ViewModels;

    using Xamarin.Forms;


    public partial class App : PrismApplication
    {
        public string Search { get; set; }

        private readonly IAccountService accountService;

        private bool TabbedNav = true;

        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            this.InitializeComponent();           
            
            if (this.TabbedNav)
            {
                this.NavigationService.NavigateAsync("TabbedHomePage");
                //var navigationPage = new NavigationPage(new TabbedHomePage());
                //this.MainPage = navigationPage;

            }
            else
            {
                this.NavigationService.NavigateAsync("NavigationPage/ProductSearch");
                //NavigationPage.SetHasNavigationBar(this, true);
                //this.MainPage.ToolbarItems.Add(new ToolbarItem("Account", null, this.ToolbarItem_Account));
                //this.MainPage.ToolbarItems.Add(new ToolbarItem("Cart", "shopping.png", this.ToolbarItem_Cart));
            }
        }

        protected override void RegisterTypes()
        {
            this.Container
                .RegisterTypeForNavigation<ProductSearch, ProductSearchViewModel>()
                .RegisterTypeForNavigation<SignInPage, SignInViewModel>()
                .RegisterTypeForNavigation<CartPage, CartPageViewModel>()
                .RegisterTypeForNavigation<ProductDetail, ProductDetailViewModel>()
                .RegisterTypeForNavigation<TabbedHomePage>()

                .RegisterType<IProductService, ProductService>()
                .RegisterType<IAccountService, AccountService>()
                .RegisterType<IPageService, PageService>()
                .RegisterType<IClientService, ClientService>(new ContainerControlledLifetimeManager())
                .RegisterType<ICartService, CartService>();
        }

        private async Task CheckAuthentication()
        {
            //var result = await this.accountService.IsAuthenticated();
            //var session = await this.accountService.GetSession();
        }

        private void ToolbarItem_Account()
        {
            this.NavigationService.NavigateAsync("SignInPage");
        }

        private void ToolbarItem_Cart()
        {
            this.NavigationService.NavigateAsync("CartPage");
        }

        protected override void OnStart()
        {
            if (Application.Current.Properties.ContainsKey("search"))
            {
                this.Search = Application.Current.Properties["search"]?.ToString();
            }
        }

        protected override void OnSleep()
        {
            Application.Current.Properties["search"] = this.Search;
        }

        protected override void OnResume()
        {
            if (Application.Current.Properties.ContainsKey("search"))
            {
                this.Search = Application.Current.Properties["search"]?.ToString();
            }
        }
    }
}
