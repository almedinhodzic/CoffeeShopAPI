using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Products;

namespace CoffeeShopAPI.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ProductDetailsDto?> GetProductDetailsAsync(int id);
    }
}
