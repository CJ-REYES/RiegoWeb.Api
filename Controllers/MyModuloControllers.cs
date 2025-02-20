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
    public class MyModuloControllers : ControllerBase
    {
        private readonly MyDbContext _context;

        public MyModuloControllers(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Modulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyModulos>>> GetMyModulos()
        {
            return await _context.MyModulos.ToListAsync();
        }

        // GET: api/Modulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyModulos>> GetMyModulo(int id)
        {
            var myModulos = await _context.MyModulos.FindAsync(id);

            if (myModulos == null)
            {
                return NotFound(new { message = "Módulo no encontrado." });
            }

            return myModulos;
        }

        // POST: api/Modulos
     [HttpPost]
public async Task<ActionResult<MyModulos>> CrearMyModulo([FromBody] MyModulosRequest request)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(new { message = "Datos del módulo no válidos." });
    }

    // Crear un nuevo objeto MyModulos usando los identificadores
    var myModulo = new MyModulos
    {
        Id_User = request.Id_User,
        Id_Modulo = request.Id_Modulo,
        Name = "Mi Modulo"
    };

    _context.MyModulos.Add(myModulo);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetMyModulo), new { id = myModulo.IdMyModulo }, myModulo);
}



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

            // Actualizar los valores permitidos
            myModulo.Name = request.Name; // Si deseas que el nombre sea personalizable, usa request.Name

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyModuloExists(id))
                {
                    return NotFound(new { message = "Módulo no encontrado." });
                }
                else
                {
                    throw;
                }
            }

            return Ok(myModulo);
        }

        // DELETE: api/Modulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMyModulo(int id)
        {
            var myModulos = await _context.MyModulos.FindAsync(id);
            if (myModulos == null)
            {
                return NotFound(new { message = "Módulo no encontrado." });
            }

            _context.MyModulos.Remove(myModulos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si el módulo existe
        private bool MyModuloExists(int id)
        {
            return _context.MyModulos.Any(e => e.IdMyModulo == id);
        }
    }
}