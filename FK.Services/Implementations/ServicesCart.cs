using FK.Services.Contracts;
using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;

namespace FK.Services.Implementations
{
    public class ServicesCart : IServicesCart
    {
        private readonly IRepositoryCarts _repositoryCarts;
        private readonly IServicesProduct _servicesProduct;

        public ServicesCart(
            IRepositoryCarts repositoryCarts,
            IServicesProduct servicesProduct
            )
        {
            _repositoryCarts = repositoryCarts;
            _servicesProduct = servicesProduct;
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
            var updatedCart = await _repositoryCarts.UpdateAsync(cart);
            return updatedCart;
        }

        public async Task<Cart?> CreateNewCart()
        {
            return await _repositoryCarts.CreateAsync(new Cart());
        }

    }
}
