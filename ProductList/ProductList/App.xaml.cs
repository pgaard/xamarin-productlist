using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ProductList
{
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using ProductList.Services;

    public partial class App : Application
    {
        public static IUnityContainer Container;

        public App()
        {            
            this.SetUpUnity();
            InitializeComponent();

            this.MainPage = new NavigationPage(new ProductSearchPage());
        }

        protected void SetUpUnity()
        {
            Container = new UnityContainer();
            Container.RegisterType<IProductService, ProductService>()
                .RegisterType<IAccountService, AccountService>()
                .RegisterType<IPageService, PageService>()
                .RegisterType<IClientService, ClientService>();
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
