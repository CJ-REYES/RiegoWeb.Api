using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;


namespace RiegoWeb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsuarioController(MyDbContext context)
        {
            _context = context;
        }
        // POST: api/Usuario/login

public class UserCreateDTO
{
    
    public required string Username { get; set; }

   
    public required string Email { get; set; }


    public required string Password { get; set; }
}
[HttpPost("login")]
public async Task<ActionResult<User>> Login([FromBody] LoginRequest loginRequest)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(new { message = "Datos de login no válidos." });
    }

    // Buscar el usuario por correo y contraseña
    var usuario = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == loginRequest.EmaiL && u.Password == loginRequest.Password);

    if (usuario == null)
    {
        return Unauthorized(new { message = "Credenciales incorrectas." });
    }

    // Devolver los datos del usuario (sin la contraseña por seguridad)
    return Ok(new
    {
        id = usuario.Id,
        correo = usuario.Email,
        nombre = usuario.Username // Asegúrate de que tu modelo User tenga esta propiedad
    });
}

// Clase para representar la solicitud de login
public class LoginRequest
{
    public string EmaiL { get; set; }
    public string Password { get; set; }
}
        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsuarios()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Usuario/5
    


        // POST: api/Usuario
        [HttpPost]
public async Task<ActionResult<User>> CreateUser(UserCreateDTO userDTO)
{
    var user = new User
    {
        Username = userDTO.Username,
        Email = userDTO.Email,
        Password = userDTO.Password
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetUsuarios), new { id = user.Id }, user);
}

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest(new { message = "El ID del usuario no coincide con el de la URL." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos del usuario no válidos." });
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound(new { message = "Usuario no encontrado." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Users.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            _context.Users.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si el usuario existe
        private bool UsuarioExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}