using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;
using System.Text.Json;

namespace FS.Infrastructure.DataAccess
{
    public class RepositoryProductExternalApi : IRepositoryProductsExternalService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "https://fakestoreapi.com";

        public async Task<Product?> GetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            string bodycontentAsString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Product>(bodycontentAsString);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/products");
            response.EnsureSuccessStatusCode();
            string bodycontentAsString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Product>>(bodycontentAsString) ?? new List<Product>();
        }

        public Task<Product> CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}