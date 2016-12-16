namespace ProductList.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Microsoft.Practices.Unity;

    using ProductList.Services;

    using Xamarin.Forms;

    public class SignInViewModel : BaseViewModel
    {
        private readonly IAccountService accountService;
        private string userName;
        private string password;
        private string message;

        public ICommand LoginCommand { get; private set; }

        public SignInViewModel()
        {
            this.accountService = App.Container.Resolve<IAccountService>();
            this.LoginCommand = new Command(async () => await this.DoLogin());            
        }

        private async Task<bool> DoLogin()
        {
            var result = await this.accountService.Authenticate(this.userName, this.password);
            
            if (result)
            {
//                result = await this.accountService.IsAuthenticated();
            }

            this.Message = result ? "Login Successful" : "Login Failed";

            return result;
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

        public string Message
        {
            get { return this.message; }
            set { this.SetValue(ref this.message, value); }
        }
    }
}
