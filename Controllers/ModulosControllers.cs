using Microsoft.AspNetCore.Mvc;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using RiegoWeb.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace RiegoWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IHubContext<RandomDataHub> _randomDataHub; // Inyectamos IHubContext<RandomDataHub>

        // CORRECCIÓN: Inyectar IHubContext<RandomDataHub>
        public ModulosController(MyDbContext context, IHubContext<RandomDataHub> randomDataHub)
        {
            _context = context;
            _randomDataHub = randomDataHub;
        }

        // GET: api/Modulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modulos>>> GetModulos()
        {
            return await _context.Modulos.ToListAsync();
        }

        // GET: api/Modulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modulos>> GetModulo(int id)
        {
            var modulo = await _context.Modulos.FindAsync(id);
            if (modulo == null) return NotFound();
            return modulo;
        }

        // POST: api/Modulos
        [HttpPost]
        public async Task<ActionResult<Modulos>> PostModulo(Modulos modulo)
        {
            _context.Modulos.Add(modulo);
            await _context.SaveChangesAsync();

            // CORRECCIÓN: Mover antes de `return`
            var historial = new HistoriaDeModulos
            {
                Id_Modulos = modulo.Id_Modulos,
                Name = modulo.Name,
                Temperatura = modulo.Temperatura,
                Humedad = modulo.Humedad,
                LuzNivel = modulo.LuzNivel,
                Fecha = DateTime.Now
            };

            _context.HistoriaDeModulos.Add(historial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModulo), new { id = modulo.Id_Modulos }, modulo);
        }

        // PUT: api/Modulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModulo(int id, Modulos modulo)
        {
            if (id != modulo.Id_Modulos) return BadRequest();

            _context.Entry(modulo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var historial = new HistoriaDeModulos
                {
                    Id_Modulos = modulo.Id_Modulos,
                    Name = modulo.Name,
                    Temperatura = modulo.Temperatura,
                    Humedad = modulo.Humedad,
                    LuzNivel = modulo.LuzNivel,
                    Fecha = DateTime.Now
                };

                _context.HistoriaDeModulos.Add(historial);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuloExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Modulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModulo(int id)
        {
            var modulo = await _context.Modulos.FindAsync(id);
            if (modulo == null) return NotFound();

            _context.Modulos.Remove(modulo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuloExists(int id)
        {
            return _context.Modulos.Any(e => e.Id_Modulos == id);
        }

        // Generar módulos aleatorios
        [HttpPost("generar")]
        public async Task<ActionResult> GenerarModulosAleatorios()
        {
            // Llamada asincrónica al Hub para generar los módulos
            await _randomDataHub.Clients.All.SendAsync("Generar100ModulosRandom");

            // Devolver una respuesta indicando que la acción se realizó
            return Ok("Modulos generados y enviados.");
        }
    }
}
