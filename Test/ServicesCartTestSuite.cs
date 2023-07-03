using FK.Services.Contracts;
using FK.Services.Implementations;
using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test
{
    public class ServicesCartTestSuite
    {

        private readonly ServicesCart _servicesCart;
        private readonly Mock<ILogger<ServicesCart>> _loggerMock = new Mock<ILogger<ServicesCart>>();
        private readonly Mock<IServicesProduct> _servicesProductMock = new Mock<IServicesProduct>();
        private readonly Mock<IRepositoryCarts> _repositoryCartsMock = new Mock<IRepositoryCarts>();

        public ServicesCartTestSuite()
        {
            _servicesCart = new ServicesCart(_repositoryCartsMock.Object, _servicesProductMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task AddProductToCart()
        {
            //Arrange
            Product productMock = new Product
            {
                Id = 1,
                Title = "A new product",
                Price = 10
            };

            Cart cartMock = new Cart
            {
                Id = 1,
                Products = new List<Product>()
            };

            _servicesProductMock.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(() => { return productMock; });
            _repositoryCartsMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(() => { return cartMock; });

            //Act
            Cart? cart = await _servicesCart.AddProductToCart(1, 1);

            //Assert
            Assert.Equal(cart?.TotalCost, 10);
        }
    }
}