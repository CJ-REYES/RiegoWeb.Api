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
           

            return await _context.MyModulos
                                 .Where(m => m.Id_User == userId)
                                 .ToListAsync();
        }

        // GET: api/MyModulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyModulos>> GetMyModulo(int id)
        {
           

            var myModulo = await _context.MyModulos
                                         .Where(m => m.IdMyModulo == id )
                                         .FirstOrDefaultAsync();

            if (myModulo == null)
            {
                return NotFound(new { message = "Módulo no encontrado o no pertenece al usuario." });
            }

            return myModulo;
        }

        [HttpPost]
        public async Task<ActionResult<MyModulos>> CrearMyModulo([FromBody] MyModulosRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos del módulo no válidos." });
            }


    // Buscar el módulo con el IdModuloIot
var modulo = await _context.Modulos
                           .Where(m => m.Id_Modulos == request.Id_Modulo) // Usa la propiedad correcta
                           .FirstOrDefaultAsync();

    if (modulo == null)
    {
        return NotFound(new { message = "No se encontró un módulo con el ID IoT proporcionado." });
    }

    // Verificar si el usuario existe
    var usuarioExiste = await _context.Users.AnyAsync(u => u.Id_User == request.Id_User);
    if (!usuarioExiste)
    {
        return BadRequest(new { message = "El usuario no existe." });
    }

    // Crear la relación en MyModulos
    var myModulo = new MyModulos
    {
        Id_User = request.Id_User,
        Id_Modulo = modulo.Id_Modulos, // Se obtiene el ID del módulo
        Name = request.Name
    };

            // Verificar si el usuario existe (opcional)
          

            var myModulos = new MyModulos
            {
                Id_User = request.Id_User, // Tomamos el ID del request
                Id_Modulo = request.Id_Modulo,
                Name = request.Name
            };


            _context.MyModulos.Add(myModulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyModulo), new { id = myModulo.IdMyModulo }, myModulo);
        }


        public class MyModulosRequest
        {
            public int Id_User { get; set; } // <- Agregamos esta propiedad
            public int Id_Modulo { get; set; }
            public required string Name { get; set; }
        }


        // PUT: api/MyModulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMyModulo(int id, [FromBody] MyModulosRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos del módulo no válidos." });
            }

            var myModulo = await _context.MyModulos.FindAsync(id);
            if (myModulo == null)
            {
                return NotFound(new { message = "Módulo no encontrado." });
            }

            // Se permite actualizar solo el nombre
            myModulo.Name = request.Name;

            _context.MyModulos.Update(myModulo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MyModulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMyModulo(int id)
        {
           

            var myModulo = await _context.MyModulos.FindAsync(id);
            if (myModulo == null )
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