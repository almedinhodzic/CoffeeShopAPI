using CoffeeShopAPI.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopAPI.Models.Products
{
    public class ProductDetailsDto : BaseProductDto
    {
        public int Id { get; set; }
        public Category? Category { get; set; }
    }
}
