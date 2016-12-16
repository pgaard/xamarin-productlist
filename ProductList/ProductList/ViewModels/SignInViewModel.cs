using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.ViewModels
{
    using System.Windows.Input;

    using ProductList.Services;

    using Xamarin.Forms;

    public class SignInViewModel : BaseViewModel
    {
        private readonly IAccountService accountService;
        private string userName;
        private string password;

        public ICommand LoginCommand { get; private set; }

        public SignInViewModel(IAccountService accountService)
        {
            this.accountService = accountService;
            this.LoginCommand = new Command(async () => await this.DoLogin());            
        }

        private async Task DoLogin()
        {
            var result = await this.accountService.Authenticate(this.userName, this.password);
        }

        public string UserName
        {
            get { return this.userName; }
            set { this.SetValue(ref this.userName, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { this.SetValue(ref this.password, value); }
        }
    }
}
