using FS.Domain.Entities.Entities;
using FS.Infrastructure.DataAccess;

namespace Test.Repository
{
    public class RepositoryProductPersistentTestSuite
    {
        private readonly RepositoryProductPersistent _repositoryProductPersistent;

        public RepositoryProductPersistentTestSuite()
        {
            _repositoryProductPersistent = new RepositoryProductPersistent();
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Price = 1 },
                new Product { Id = 2, Price = 2 },
                new Product { Id = 3, Price = 3 },
            };

            // Act
            var result = await _repositoryProductPersistent.AddBulkAsync(products);

            // Assert
            Assert.Equal(products.Count, result.ToList().Count);

        }
    }
}