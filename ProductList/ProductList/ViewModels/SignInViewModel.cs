namespace ProductList.ViewModels
{
    using System.Threading.Tasks;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    using ProductList.Services;    

    public class SignInViewModel : BindableBase, INavigationAware
    {
        private readonly IAccountService accountService;
        private string userName;
        private string password;
        private string message;

        public DelegateCommand LoginCommand { get; private set; }

        public SignInViewModel(IAccountService accountService)
        {
            this.accountService = accountService;
            this.LoginCommand = new DelegateCommand(async () => await this.DoLogin());            
        }

        private async Task<bool> DoLogin()
        {
            var result = await this.accountService.Authenticate(this.userName, this.password);
            
            if (result)
            {
                ///result = await this.accountService.IsAuthenticated();
            }

            this.Message = result ? "Login Successful" : "Login Failed";

            return result;
        }

        public string UserName
        {
            get { return this.userName; }
            set { this.SetProperty(ref this.userName, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { this.SetProperty(ref this.password, value); }
        }

        public string Message
        {
            get { return this.message; }
            set { this.SetProperty(ref this.message, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
