using FK.Services.Contracts;
using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;

namespace FK.Services
{
    public class ServicesProduct : IServicesProduct
    {
        private readonly IRepositoryProducts _repositoryProducts;

        public ServicesProduct(IRepositoryProducts repositoryProducts)
        {
            _repositoryProducts = repositoryProducts;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {

            try
            {
                // Call external API
                var products = await _repositoryProducts.GetAllAsync();

                // Save data to backup
                var createdProducts = await _repositoryProducts.CreateAsync(products);

                // Return data to client
                var a = await new HttpClient().GetAsync("google.com");

            }
            catch (HttpRequestException ex)
            {
                // Log error

                // Try and get info from backup

                // If no data is found return null

                // Return information

                // catch error and log
            }
            catch (Exception ex)
            {
                // Log error
            }

            await Task.Run(() => { });

            return new List<Product>()
            {
                new Product { Id = 1, Description = "product1"},
                new Product { Id = 2, Description = "product2"},
                new Product { Id = 3, Description = "product3"},
            };
        }

    }
}