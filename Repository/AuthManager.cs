using AutoMapper;
using CoffeeShopAPI.Common;
using CoffeeShopAPI.Contracts;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShopAPI.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        public AuthManager(UserManager<Employee> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> Login(LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.Email);
            if(user == null)
            {
                return false;
            }
            var passwordCorrect = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if(!passwordCorrect)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<IdentityError>> Register(RegisterUserDto userDto)
        {
            var user = _mapper.Map<RegisterUserDto, Employee>(userDto);
            user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }

            return result.Errors;
        }
    }
}
