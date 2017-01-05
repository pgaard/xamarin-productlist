namespace ProductList
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Practices.Unity;

    using ProductList.Services;
    using ProductList.ViewModels;

    using Xamarin.Forms;


    public partial class App : Application
    {
        public static IUnityContainer Container;
        
        public string Search { get; set; }

        private readonly IPageService pageService;
        private readonly IAccountService accountService;

        private bool TabbedNav = true;

        public App()
        {
            try
            {
                this.SetUpDependencies();
                this.InitializeComponent();

                this.pageService = Container.Resolve<IPageService>();

                if (this.TabbedNav)
                {
                    var navigationPage = new NavigationPage(new TabbedHomePage());                    
                    this.MainPage = navigationPage;

                }
                else
                {
                    this.MainPage = new NavigationPage(new ProductSearchPage());
                    this.MainPage.ToolbarItems.Add(new ToolbarItem("Account", null, this.ToolbarItem_Account));
                    this.MainPage.ToolbarItems.Add(new ToolbarItem("Cart", "shopping.png", this.ToolbarItem_Cart));
                }

                this.accountService = App.Container.Resolve<IAccountService>();
                //Task.Factory.StartNew(this.CheckAuthentication);
            }
            catch (Exception ex)
            {
                                
            }
        }

        private async Task CheckAuthentication()
        {
            //var result = await this.accountService.IsAuthenticated();
            //var session = await this.accountService.GetSession();
        }

        private void ToolbarItem_Account()
        {
            this.pageService.PushAsync(new SignInPage());
        }

        private void ToolbarItem_Cart()
        {
            this.pageService.PushAsync(new CartPage());
        }

        protected void SetUpDependencies()
        {
            Container = new UnityContainer();

            Container.RegisterType<IProductService, ProductService>()
                .RegisterType<IAccountService, AccountService>()
                .RegisterType<IPageService, PageService>()
                .RegisterType<IClientService, ClientService>(new ContainerControlledLifetimeManager())
                .RegisterType<ICartService, CartService>()
                .RegisterType<CartPageViewModel>()
                .RegisterType<ProductSearchViewModel>()
                .RegisterType<SignInViewModel>()
                .RegisterType<ProductDetailViewModel>();
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
