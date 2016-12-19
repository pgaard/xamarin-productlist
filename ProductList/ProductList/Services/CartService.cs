namespace ProductList.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Practices.Unity;

    using Newtonsoft.Json;

    using ProductList.Models;

    using Xamarin.Forms;

    public class CartService : ICartService
    {
        private IClientService client;

        public CartService()
        {
            this.client = App.Container.Resolve<IClientService>();
        }

        public async Task<bool> AddToCart(Product product)
        {
            var cartLines = new { productId = product.id, qtyOrdered = 1, unitOfMeasure = product.unitOfMeasure };
            var content = new StringContent(JsonConvert.SerializeObject(cartLines), Encoding.UTF8,"application/json");            
            var result = await this.client.PostAsync("api/v1/carts/current/cartlines", content);
            return result.StatusCode == HttpStatusCode.Created;
        }

        public async Task<Cart> GetCart()
        {
            try
            {
                var result = await this.client.GetAsync("api/v1/carts/current?expand=cartlines,costcodes,shipping,tax");
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                var response = await result.Content.ReadAsStringAsync();
                var cart = JsonConvert.DeserializeObject<Cart>(response);

                foreach (var cartline in cart.cartLines)
                {
                    cartline.ProductSmallImageSource = new UriImageSource { Uri = new Uri(cartline.smallImagePath), CachingEnabled = true };
                }

                return cart;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
