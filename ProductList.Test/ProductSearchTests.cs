namespace ProductList.Test
{
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
    }
}
