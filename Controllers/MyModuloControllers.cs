using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;

namespace RiegoWeb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyModuloControllers : ControllerBase
    {
        private readonly MyDbContext _context;

        public MyModuloControllers(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/MyModulos (Obtener todos los módulos del usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyModulos>>> GetMyModulos()
        {
            var userId = ObtenerIdUsuarioAutenticado();
            if (userId == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado." });
            }

            return await _context.MyModulos
                                 .Where(m => m.Id_User == userId)
                                 .ToListAsync();
        }

        // GET: api/MyModulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyModulos>> GetMyModulo(int id)
        {
            var userId = ObtenerIdUsuarioAutenticado();
            if (userId == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado." });
            }

            var myModulo = await _context.MyModulos
                                         .Where(m => m.IdMyModulo == id && m.Id_User == userId)
                                         .FirstOrDefaultAsync();

            if (myModulo == null)
            {
                return NotFound(new { message = "Módulo no encontrado o no pertenece al usuario." });
            }

            return myModulo;
        }

        // POST: api/MyModulos (Crear módulo para el usuario autenticado)
        [HttpPost]
        public async Task<ActionResult<MyModulos>> CrearMyModulo([FromBody] MyModulosRequest request)
        {
            var userId = ObtenerIdUsuarioAutenticado();
            if (userId == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos del módulo no válidos." });
            }

            var myModulo = new MyModulos
            {
                Id_User = userId.Value, // Se asigna automáticamente el usuario autenticado
                Id_Modulo = request.Id_Modulo,
                Name = request.Name
            };

            _context.MyModulos.Add(myModulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyModulo), new { id = myModulo.IdMyModulo }, myModulo);
        }

        // PUT: api/MyModulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMyModulo(int id, [FromBody] MyModulosRequest request)
        {
            var userId = ObtenerIdUsuarioAutenticado();
            if (userId == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos del módulo no válidos." });
            }

            var myModulo = await _context.MyModulos.FindAsync(id);
            if (myModulo == null || myModulo.Id_User != userId)
            {
                return NotFound(new { message = "Módulo no encontrado o no pertenece al usuario." });
            }

            // Se permite actualizar solo el nombre
            myModulo.Name = request.Name;

            await _context.SaveChangesAsync();

            return Ok(myModulo);
        }

        // DELETE: api/MyModulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMyModulo(int id)
        {
            var userId = ObtenerIdUsuarioAutenticado();
            if (userId == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado." });
            }

            var myModulo = await _context.MyModulos.FindAsync(id);
            if (myModulo == null || myModulo.Id_User != userId)
            {
                return NotFound(new { message = "Módulo no encontrado o no pertenece al usuario." });
            }

            _context.MyModulos.Remove(myModulo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para obtener el ID del usuario autenticado
        private int? ObtenerIdUsuarioAutenticado()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
        }
    }
}
