using Microsoft.EntityFrameworkCore;

namespace CoffeeShopAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Order>? Orders { get; set; }
    }
}
