using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;
using System.Text.Json;

namespace FS.Infrastructure.DataAccess
{
    public class RepositoryCartPersistent : IRepositoryCarts
    {
        private readonly string _storageFileName = "cartStorage.txt";
        private readonly string _path;
        public RepositoryCartPersistent()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
        }

        private async Task<int> GetLastId()
        {
            List<Cart> items = await GetDeserializeItems();
            return items.LastOrDefault()?.Id ?? 1;
        }

        private async Task SaveData(IEnumerable<Cart> items)
        {
            var payloadAsString = JsonSerializer.Serialize(items);
            await File.WriteAllTextAsync(_path, payloadAsString);
            return;
        }

        private async Task<List<Cart>> GetDeserializeItems()
        {
            string payload = await File.ReadAllTextAsync(_path);
            List<Cart>? deserializeItems = JsonSerializer.Deserialize<List<Cart>>(payload);
            return deserializeItems ?? new List<Cart>();
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await GetDeserializeItems() ?? new List<Cart>();
        }

        public async Task<Cart?> GetAsync(int id)
        {
            var items = await GetDeserializeItems();
            return items.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Cart?> UpdateAsync(Cart cart)
        {
            List<Cart> items = await GetDeserializeItems();
            int itemIndex = items.FindIndex(x => x.Id == cart.Id);

            if (itemIndex < 0)
            {
                return null;
            }

            items[itemIndex] = cart;
            await SaveData(items);
            return cart;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            IEnumerable<Cart> carts = await GetAllAsync();
            var cartsList = carts.ToList();
            Cart? cartToDelete = carts.FirstOrDefault(x => x.Id == id);

            if (cartToDelete is null)
            {
                return false;
            }

            bool result = cartsList.Remove(cartToDelete);
            if (result)
            {
                await SaveData(cartsList);
            }
            return result;
        }

        public async Task<Cart> CreateAsync(Cart cart)
        {
            List<Cart> cartList = await GetDeserializeItems();
            cart.Id = await GetLastId() + 1;
            cartList.Add(cart);
            await SaveData(cartList);

            return cart;
        }

    }
}
