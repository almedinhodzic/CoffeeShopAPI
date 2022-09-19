using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShopAPI.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(RegisterUserDto userDto);
        Task<AuthResponseDto?> Login(LoginUserDto loginUserDto);
    }
}
