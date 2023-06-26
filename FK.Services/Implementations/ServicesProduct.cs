using FK.Services.Contracts;
using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;

namespace FK.Services
{
    public class ServicesProduct : IServicesProduct
    {
        private readonly IRepositoryProductsExternalService _repositoryProductsExternal;
        private readonly IRepositoryProducts _repositoryProductsDatabase;

        public ServicesProduct(
            IRepositoryProductsExternalService repositoryProductsExternal,
            IRepositoryProducts repositoryProductsDatabase
            )
        {
            _repositoryProductsExternal = repositoryProductsExternal;
            _repositoryProductsDatabase = repositoryProductsDatabase;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                // Call external API
                var products = await _repositoryProductsExternal.GetAllAsync();

                // Save data to backup
                await _repositoryProductsDatabase.AddBulkAsync(products);

                return products;
            }
            catch (HttpRequestException ex)
            {
                // TODO -> _logger.Log(ex.message)

                // Get info from backup
                IEnumerable<Product> products = await _repositoryProductsDatabase.GetAllAsync();
                return products;
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            Product? product;

            try
            {
                // Call external API
                product = await _repositoryProductsExternal.GetAsync(id);

                // Save data to backup
                if (product is not null)
                {
                    await _repositoryProductsDatabase.UpdateAsync(product);
                }

                return product;
            }
            catch (HttpRequestException ex)
            {
                // TODO -> _logger.Log(ex.message)
                // Get info from backup
                return await _repositoryProductsDatabase.GetAsync(id);
            }

        }

    }
}