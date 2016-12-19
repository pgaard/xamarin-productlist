namespace ProductList
{
    using Microsoft.Practices.Unity;

    using ProductList.Services;

    using Xamarin.Forms;


    public partial class App : Application
    {
        public static IUnityContainer Container;
        
        public string Search { get; set; }

        public App()
        {            
            this.SetUpUnity();
            this.InitializeComponent();
            this.MainPage = new NavigationPage(new ProductSearchPage());
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
                this.Search = Application.Current.Properties["search"].ToString();
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
                this.Search = Application.Current.Properties["search"].ToString();
            }
        }
    }
}
