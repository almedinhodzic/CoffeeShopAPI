using CoffeeShopAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopAPI.Models.Products
{
    public class BaseProductDto
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
