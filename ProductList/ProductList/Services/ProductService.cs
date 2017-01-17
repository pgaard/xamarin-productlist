namespace ProductList.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    using Microsoft.Practices.Unity;

    using ProductList.Models;

    using Xamarin.Forms;
    
    public class ProductService : IProductService
    {
        private readonly IClientService client;

        public ProductService(IClientService clientService)
        {
            this.client = clientService;
        }

        public async Task<ProductCollection> DoProductSearch(string term, int page)
        {
            try
            {
                var content = await this.client.GetAsync($"api/v1/products/?pageSize=32&page={page}&query={term}&expand=htmlcontent");

                var productCollection = new ProductCollection();
                if (content.StatusCode != HttpStatusCode.NotFound)
                {
                    var result = await content.Content.ReadAsStringAsync();
                    try
                    {
                        productCollection = JsonConvert.DeserializeObject<ProductCollection>(result);
                        foreach (var product in productCollection.products)
                        {
                            product.ProductSmallImageSource = new UriImageSource
                                                              {
                                                                  Uri = new Uri(product.smallImagePath),
                                                                  CachingEnabled = true
                                                              };
                            product.ProductLargeImageSource = new UriImageSource
                                                              {
                                                                  Uri = new Uri(product.largeImagePath),
                                                                  CachingEnabled = true
                                                              };
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
                return productCollection;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
