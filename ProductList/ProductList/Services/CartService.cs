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

            this.client.StoreState(); // whats the best place for this?

            return result.StatusCode == HttpStatusCode.Created;
        }

        public async Task<bool> DeleteFromCart(string id)
        {
            try
            {
                var result = await this.client.DeleteAsync("api/v1/carts/current/cartlines/" + id);
                return result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                return false;
            }

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

                return await GetCartFromResponse(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static async Task<Cart> GetCartFromResponse(HttpResponseMessage result)
        {
            var response = await result.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<Cart>(response);

            if (cart.cartLines != null)
            {
                foreach (var cartline in cart.cartLines)
                {
                    cartline.ProductSmallImageSource = new UriImageSource
                                                       {
                                                           Uri = new Uri(cartline.smallImagePath),
                                                           CachingEnabled = true
                                                       };
                }
            }
            return cart;
        }

        public async Task<Cart> PatchCartline(CartLine cartline)
        {
            try
            {
                cartline.ProductSmallImageSource = null;
                var content = new StringContent(JsonConvert.SerializeObject(cartline), Encoding.UTF8, "application/json");

                var result = await this.client.PatchAsync($"api/v1/carts/current/cartlines/{cartline.id}", content);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                return await GetCartFromResponse(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
