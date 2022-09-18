using Microsoft.AspNetCore.Identity;

namespace CoffeeShopAPI.Data
{
    public class Employee : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
