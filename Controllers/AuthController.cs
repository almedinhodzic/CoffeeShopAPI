using CoffeeShopAPI.Contracts;
using CoffeeShopAPI.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var errors = await _authManager.Register(registerUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var authResponse = await _authManager.Login(loginUserDto);
            if (authResponse == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return Unauthorized(ModelState);
            }

            return Ok(authResponse);
        }

        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok();
        }


    }
}
