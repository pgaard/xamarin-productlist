namespace ProductList.Services
{
    using System.Threading.Tasks;

    using ProductList.Models;

    public interface IAccountService
    {
        Task<bool> Authenticate(string userName, string password);

        Task<Session> GetSession();
        Task<bool> IsAuthenticated();
    }
}
