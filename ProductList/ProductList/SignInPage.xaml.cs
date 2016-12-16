using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProductList
{
    using System.Windows.Input;

    using ProductList.Services;
    using ProductList.ViewModels;

    public partial class SignInPage : ContentPage
    {
        public SignInViewModel ViewModel
        {
            get { return BindingContext as SignInViewModel; }
            set { BindingContext = value; }
        }

        public SignInPage()
        {
            this.ViewModel = new SignInViewModel();
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            this.ViewModel.LoginCommand.Execute(sender);
            //DisplayAlert("sign in", this.ViewModel.UserName + " " + this.ViewModel.Password, "OK");
        }
    }
}
