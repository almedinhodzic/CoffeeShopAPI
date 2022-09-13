using CoffeeShopAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopAPI.Models.Orders
{
    public class BaseOrderDto
    {
        [Required]
        public List<Product>? Products { get; set; }
    }
}
