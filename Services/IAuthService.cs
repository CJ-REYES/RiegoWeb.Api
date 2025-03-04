using RiegoWeb.Api.Models;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string correo, string contraseña);
}
