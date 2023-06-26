using FS.Domain.Entities.Entities;

namespace FS.Domain.Entities.Contracts
{
    public interface IRepositoryProducts : IRepository<Product>
    {
        Task<IEnumerable<Product>> AddBulkAsync(IEnumerable<Product> products);
    }
}
