using System.ComponentModel.DataAnnotations;

namespace CoffeeShopAPI.Models.Categories
{
    public class BaseCategoryDto
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
