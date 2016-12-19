using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Services
{
    using ProductList.Models;

    public interface ICartService
    {
        Task<bool> AddToCart(Product product);
        Task<Cart> GetCart();
    }
}
