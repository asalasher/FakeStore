using FK.Services.Contracts;
using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;
using Microsoft.Extensions.Logging;

namespace FK.Services.Implementations
{
    public class ServicesCart : IServicesCart
    {
        private readonly IRepositoryCarts _repositoryCarts;
        private readonly IServicesProduct _servicesProduct;
        private readonly ILogger<ServicesCart> _logger;

        public ServicesCart(
            IRepositoryCarts repositoryCarts,
            IServicesProduct servicesProduct,
            ILogger<ServicesCart> logger
            )
        {
            _repositoryCarts = repositoryCarts;
            _servicesProduct = servicesProduct;
            _logger = logger;
        }

        public async Task<Cart?> AddProductToCart(int idCart, int idProduct)
        {
            Product? product = await _servicesProduct.GetProductById(idProduct);
            Cart? cart = await _repositoryCarts.GetAsync(idCart);
            if (cart is null || product is null)
            {
                throw new ArgumentException("Item or cart not found in repositories");
            }
            cart.AddProduct(product);
            await _repositoryCarts.UpdateAsync(cart);
            return cart;
        }

        public async Task<Cart?> CreateNewCart()
        {
            return await _repositoryCarts.CreateAsync(new Cart());
        }

    }
}
