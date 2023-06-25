using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;

namespace FK.Services.Implementations
{
    public class ServicesCart
    {
        private readonly IRepositoryCarts _repositoryCarts;
        private readonly IRepositoryProducts _repositoryProducts;

        public ServicesCart(
            IRepositoryCarts repositoryCarts,
            IRepositoryProducts repositoryProducts
            )
        {
            _repositoryCarts = repositoryCarts;
            _repositoryProducts = repositoryProducts;
        }

        public async Task<bool> AddProductToCart(int idCart, int idProduct)
        {
            try
            {
                Cart? cart = await _repositoryCarts.GetAsync(idCart);
                Product? product = await _repositoryProducts.GetAsync(idProduct);

                if (cart is null || product is null)
                {
                    throw new ArgumentException("Item not found in repositories");
                }
                return true;
            }
            catch (Exception e)
            {
                // TODO -> _logger.LogError(ex.message);
                return false;
            }
        }

        public async Task<Cart?> CreateNewCart()
        {
            try
            {
                Cart cart = await _repositoryCarts.CreateAsync(new Cart());
                return cart;
            }
            catch (Exception e)
            {
                // TODO -> _logger.LogError(ex.message);
                return null;
            }
        }

    }
}
