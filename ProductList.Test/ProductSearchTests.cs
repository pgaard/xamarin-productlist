namespace ProductList.Test
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using ProductList.Models;
    using ProductList.Services;
    using ProductList.ViewModels;

    [TestFixture]
    public class ProductSearchTests
    {
        private ProductSearchViewModel model;
        private Mock<IProductService> productServiceMock;
        private Mock<IPageService> pageServiceMock;

        [SetUp]
        public void SetUp()
        {            
            this.productServiceMock = new Mock<IProductService>();
            this.pageServiceMock = new Mock<IPageService>();
            this.model = new ProductSearchViewModel(this.productServiceMock.Object, this.pageServiceMock.Object);
        }

        [Test]
        public async Task SelectProduct_Navigates_To_Product_Detail()
        {
            var product = new Product();

            await this.model.SelectProduct(product);

            this.pageServiceMock.Verify(p => p.PushAsync("ProductDetail", It.IsAny<ParameterOverride[]>()), Times.Once());
        }

        [Test]
        public void DoSearch_Sets_Products()
        {
            var productCollection = new ProductCollection() { products = new List<Product>() { new Product { name = "product a"} } };
            this.productServiceMock.Setup(p => p.DoProductSearch("test", 1)).ReturnsAsync(productCollection);

            this.model.DoSearchCommand.Execute("test");

            Assert.IsNotNull(this.model.Products);
            Assert.AreEqual(1, this.model.Products.Count);
            Assert.AreEqual(productCollection.products[0], this.model.Products[0]);
            Assert.IsFalse(this.model.IsSearching);
        }

        [Test]
        public void ItemAppearing_Loads_More_Products()
        {
            var product = new Product();
            var products = new Product[10].ToList();
            products.Add(product);
            
            var productCollection = new ProductCollection()
                                    {
                                        products = products,
                                        pagination = new Pagination()
                                                     {
                                                         numberOfPages = 10
                                                     }
                                    };

            this.productServiceMock.Setup(p => p.DoProductSearch("test", 1)).ReturnsAsync(productCollection);

            this.model.DoSearchCommand.Execute("test");
            this.model.ItemAppearingCommand.Execute(product);

            this.productServiceMock.Verify(x => x.DoProductSearch("test", 1));
            this.productServiceMock.Verify(x => x.DoProductSearch("test", 2));
        }
    }
}
