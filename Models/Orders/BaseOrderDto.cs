using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopAPI.Models.Orders
{
    public class BaseOrderDto
    {
        [Required]
        public List<ProductDto>? Products { get; set; }
    }
}
