using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Orders;

namespace CoffeeShopAPI.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<OrderDTO>?> GetAllOrdersDetailsAsync();
        Task<OrderDTO?> GetOrderDetailsAsync(int id);
    }
}
