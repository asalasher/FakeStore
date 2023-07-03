using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;
using System.Text.Json;

namespace FS.Infrastructure.DataAccess
{
    public class RepositoryProductPersistent : IRepositoryProducts
    {
        private readonly string _storageFileName = "productStorage.txt";
        private readonly string _path;

        public RepositoryProductPersistent()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
        }

        private async Task<List<Product>> GetDeserializeItems()
        {
            string payload = await File.ReadAllTextAsync(_path);
            List<Product>? deserializeItems = JsonSerializer.Deserialize<List<Product>>(payload);
            return deserializeItems ?? new List<Product>();
        }

        private async Task SaveData(IEnumerable<Product> products)
        {
            string payloadAsString = JsonSerializer.Serialize(products);
            await File.WriteAllTextAsync(_path, payloadAsString);
            return;
        }

        public async Task<IEnumerable<Product>> AddBulkAsync(IEnumerable<Product> products)
        {
            await SaveData(products);
            return products;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await GetDeserializeItems();
        }

        public async Task<Product?> GetAsync(int id)
        {
            List<Product> items = await GetDeserializeItems();
            return items.FirstOrDefault(x => x.Id == id);
        }

        public Task<Product?> UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

    }
}
