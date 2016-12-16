namespace ProductList.Services
{
    using System.Threading.Tasks;

    public interface IAccountService
    {
        Task<bool> Authenticate(string userName, string password);
    }
}
