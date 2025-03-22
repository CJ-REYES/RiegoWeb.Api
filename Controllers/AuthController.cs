using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RiegoWeb.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string EmaiL, [FromQuery] string Password)
        {
            var token = await _authService.AuthenticateAsync(EmaiL, Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            return Ok(new { token });
        }
    }
}
