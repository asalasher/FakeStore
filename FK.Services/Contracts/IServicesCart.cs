using FS.Domain.Entities.Entities;

namespace FK.Services.Contracts
{
    public interface IServicesCart
    {
        Task<Cart?> AddProductToCart(int idCart, int idProduct);
        Task<Cart?> CreateNewCart();
    }
}