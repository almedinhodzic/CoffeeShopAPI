using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopAPI.Data
{
    public class Product : Base
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Description { get; set; }
        public List<Order>? Orders { get; set; }
        public string? ImageName { get; set; }
    }
}
