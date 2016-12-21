namespace ProductList
{
    using System;

    using Microsoft.Practices.Unity;

    using ProductList.Services;

    using Xamarin.Forms;


    public partial class App : Application
    {
        public static IUnityContainer Container;
        
        public string Search { get; set; }

        public App()
        {
            try
            {
                this.SetUpUnity();
                this.InitializeComponent();
                this.MainPage = new NavigationPage(new ProductSearchPage());
                this.MainPage.ToolbarItems.Add(new ToolbarItem("Account",null, this.ToolbarItem_Account));
                this.MainPage.ToolbarItems.Add(new ToolbarItem("Cart", "shopping.png", this.ToolbarItem_Cart));
            }
            catch (Exception ex)
            {
                                
            }
        }

        private void ToolbarItem_Account()
        {
            this.MainPage.Navigation.PushAsync(new SignInPage());
        }

        private void ToolbarItem_Cart()
        {
            this.MainPage.Navigation.PushAsync(new CartPage());
        }

        protected void SetUpUnity()
        {
            Container = new UnityContainer();

            Container.RegisterType<IProductService, ProductService>()
                .RegisterType<IAccountService, AccountService>()
                .RegisterType<IPageService, PageService>()
                .RegisterType<IClientService, ClientService>(new ContainerControlledLifetimeManager())
                .RegisterType<ICartService, CartService>();
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
