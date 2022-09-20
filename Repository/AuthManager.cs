using AutoMapper;
using CoffeeShopAPI.Common;
using CoffeeShopAPI.Contracts;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CoffeeShopAPI.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _config;
        public AuthManager(UserManager<Employee> userManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _config = configuration;
        }

        public async Task<string> CreateRefreshToken(Employee user)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, "CoffeeShop", "RefreshToken");
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, "CoffeeShop", "RefreshToken");
            var result = await _userManager.SetAuthenticationTokenAsync(user, "CoffeeShop", "RefreshToken", newRefreshToken);
            return newRefreshToken;
        }

        public async Task<AuthResponseDto?> Login(LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.Email);
            if (user == null)
            {
                return null;
            }
            var passwordCorrect = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if (!passwordCorrect)
            {
                return null;
            }

            var token = await GenerateJwtToken(user);
            return new AuthResponseDto
            {
                UserId = user.Id,
                Token = token,
                RefreshToken = await CreateRefreshToken(user),
            };
        }

        public async Task<IEnumerable<IdentityError>> Register(RegisterUserDto userDto)
        {
            var user = _mapper.Map<RegisterUserDto, Employee>(userDto);
            user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }

            return result.Errors;
        }

        public async Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(user, "CoffeeShop", "RefreshToken", request.RefreshToken);

            if(isValidRefreshToken)
            {
                var token = await GenerateJwtToken(user);
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = user.Id,
                    RefreshToken = await CreateRefreshToken(user),
                };
            };

            await _userManager.UpdateSecurityStampAsync(user);
            return null;
        }

        private async Task<string> GenerateJwtToken(Employee user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }.Union(roleClaims).Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );
            

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
