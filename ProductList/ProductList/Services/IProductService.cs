
namespace ProductList.Services
{
    using System.Threading.Tasks;
    using ProductList.Models;

    public interface IProductService
    {
        Task<ProductCollection> DoProductSearch(string term, int page);
    }
}
