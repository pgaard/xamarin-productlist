namespace ProductList
{
    using Microsoft.Practices.Unity;

    using ProductList.Services;

    using Xamarin.Forms;


    public partial class App : Application
    {
        public static IUnityContainer Container;

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
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
