using FK.Services.Contracts;
using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;
using Microsoft.Extensions.Logging;

namespace FK.Services
{
    public class ServicesProduct : IServicesProduct
    {
        private readonly IRepositoryProductsExternalService _repositoryProductsExternal;
        private readonly IRepositoryProducts _repositoryProductsDatabase;
        private readonly ILogger<ServicesProduct> _logger;

        public ServicesProduct(
            IRepositoryProductsExternalService repositoryProductsExternal,
            IRepositoryProducts repositoryProductsDatabase,
            ILogger<ServicesProduct> logger
            )
        {
            _repositoryProductsExternal = repositoryProductsExternal;
            _repositoryProductsDatabase = repositoryProductsDatabase;
            _logger = logger;
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
                _logger.LogError(ex.Message);

                // Get info from backup
                IEnumerable<Product> products = await _repositoryProductsDatabase.GetAllAsync();
                return products;
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _repositoryProductsDatabase.GetAsync(id);
        }

    }
}