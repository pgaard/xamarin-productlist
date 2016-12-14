namespace ProductList.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using ProductList.Models;

    using Xamarin.Forms;

    public class ProductService : IProductService
    {
        private readonly HttpClient client;

        public ProductService()
        {
            this.client = new HttpClient();
        }

        public async Task<ProductCollection> DoProductSearch(string term, int page)
        {
            var content = await this.client.GetAsync($"http://search.insitesoftqa.com/api/v1/products/?pageSize=32&page={page}&query={term}");

            var productCollection = new ProductCollection();
            if (content.StatusCode != HttpStatusCode.NotFound)
            {
                var result = await content.Content.ReadAsStringAsync();
                try
                {
                    productCollection = JsonConvert.DeserializeObject<ProductCollection>(result);
                    foreach (var product in productCollection.products)
                    {
                        product.ProductSmallImageSource = new UriImageSource { Uri = new Uri(product.smallImagePath), CachingEnabled = true };
                        product.ProductLargeImageSource = new UriImageSource { Uri = new Uri(product.largeImagePath), CachingEnabled = true };
                    }
                }
                catch 
                {                    
                }
            }
            return productCollection;
        }
    }
}
