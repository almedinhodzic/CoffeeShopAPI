using CoffeeShopAPI.Data;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopAPI.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderDTO>?> GetAllOrdersDetailsAsync()
        {
            List<Order> records = await _context.Orders.Include("OrderProducts.Product").AsNoTracking().ToListAsync();
            if(records == null)
            {
                return null;
            }

            var listOfOrders = from r in records
                               select new OrderDTO
                               {
                                   OrderId = r.Id,
                                   TableNumber = r.TableNumber,
                                   Details = from d in r.OrderProducts
                                             select new OrderDTO.Detail()
                                             {
                                                 ProductId = d.Product.Id,
                                                 ProductName = d.Product.Name,
                                                 Price = d.Product.Price,
                                                 Quantity = d.Quantity
                                             }
                               };

            return listOfOrders.ToList();
        }

        public async Task<OrderDTO?> GetOrderDetailsAsync(int id)
        {
            var record = await _context.Orders.Include("OrderProducts.Product").FirstOrDefaultAsync(q => q.Id == id);

            return new OrderDTO
            {
                OrderId = record.Id,
                TableNumber = record.TableNumber,
                Details = from d in record?.OrderProducts
                          select new OrderDTO.Detail()
                          {
                              ProductId = d.Product.Id,
                              ProductName = d.Product.Name,
                              Price = d.Product.Price,
                              Quantity = d.Quantity
                          }
            };

        }
    }
}
