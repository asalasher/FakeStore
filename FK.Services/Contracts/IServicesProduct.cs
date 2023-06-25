using FS.Domain.Entities.Entities;

namespace FK.Services.Contracts
{
    public interface IServicesProduct
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}