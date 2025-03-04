using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



public class AuthService : IAuthService
{
    private readonly MyDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(MyDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<string> AuthenticateAsync(string correo, string contraseña)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Correo == correo && u.Contraseña == contraseña);

        if (user == null)
        {
            return null; // Usuario no encontrado
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
       var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id_User.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Correo),
            new Claim(ClaimTypes.Name, user.Nombre),
            new Claim("userId", user.Id_User.ToString()) // Puedes agregar más claims si es necesario
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2), // Token válido por 2 horas
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
