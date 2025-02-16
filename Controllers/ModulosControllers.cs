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
    public class ModulosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ModulosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Modulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modulos>>> GetModulos()
        {
            return await _context.Modulo.ToListAsync();
        }

        // GET: api/Modulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modulos>> GetModulo(int id)
        {
            var modulo = await _context.Modulo.FindAsync(id);

            if (modulo == null)
            {
                return NotFound(new { message = "Módulo no encontrado." });
            }

            return modulo;
        }

        // POST: api/Modulos
        [HttpPost]
        public async Task<ActionResult<Modulos>> CrearModulo(Modulos modulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos del módulo no válidos." });
            }

            _context.Modulo.Add(modulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModulo), new { id = modulo.Id_Modulos }, modulo);
        }

        // PUT: api/Modulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarModulo(int id, Modulos modulo)
        {
            if (id != modulo.Id_Modulos)
            {
                return BadRequest(new { message = "El ID del módulo no coincide con el de la URL." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos del módulo no válidos." });
            }

            _context.Entry(modulo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuloExists(id))
                {
                    return NotFound(new { message = "Módulo no encontrado." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Modulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarModulo(int id)
        {
            var modulo = await _context.Modulo.FindAsync(id);
            if (modulo == null)
            {
                return NotFound(new { message = "Módulo no encontrado." });
            }

            _context.Modulo.Remove(modulo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si el módulo existe
        private bool ModuloExists(int id)
        {
            return _context.Modulo.Any(e => e.Id_Modulos == id);
        }
    }
}