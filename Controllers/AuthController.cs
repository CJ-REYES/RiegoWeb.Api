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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var token = await _authService.AuthenticateAsync(model.Correo, model.Contraseña);

            if (token == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            return Ok(new { token });
        }
    }

    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
