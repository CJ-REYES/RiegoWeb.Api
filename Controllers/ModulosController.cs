using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;
using System.Text.Json.Serialization;

namespace RiegoWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ModulosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Modulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuloDTO>>> GetModulos()
        {
            return await _context.Modulos
                .Include(m => m.Lecturas)
                .Select(m => new ModuloDTO
                {
                    Id_Modulo = m.Id_Modulo,
                    Name = m.Name,
                    Id_User = m.Id_User,
                    Date = m.Date,
                    CreatedAt = m.CreatedAt,
                })
                .ToListAsync();
        }

        // GET: api/Modulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuloDTO>> GetModulo(int id)
        {
            var modulo = await _context.Modulos
                .Include(m => m.Lecturas)
                .FirstOrDefaultAsync(m => m.Id_Modulo == id);

            if (modulo == null) return NotFound();

            return new ModuloDTO
            {
                Id_Modulo = modulo.Id_Modulo,
                Name = modulo.Name,
                Id_User = modulo.Id_User,
                Date = modulo.Date,
                CreatedAt = modulo.CreatedAt,
                Lecturas = modulo.Lecturas.Select(l => new LecturaModuloDTO
                {
                    Id = l.Id,
                    Temperatura = l.Temperatura,
                    Humedad = l.Humedad,
                    NivelLux = l.NivelLux,
                    Date = l.Date
                }).ToList()
            };
        }

        // POST: api/Modulos
        [HttpPost]
        public async Task<ActionResult<Modulos>> CreateModulo(CreateModuloDTO moduloDTO)
        {
            var modulo = new Modulos
            {
                Name = moduloDTO.Name,
                Id_User = moduloDTO.Id_User,
                Date = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            _context.Modulos.Add(modulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModulo), new { id = modulo.Id_Modulo }, modulo);
        }

        // PUT: api/Modulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModulo(int id, UpdateModuloDTO updateDto)
        {
            var modulo = await _context.Modulos.FindAsync(id);
            if (modulo == null) return NotFound();

            modulo.Name = updateDto.Name;
            await _context.SaveChangesAsync();

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

        // GET: api/Modulos/{idModule}/lecturas
        [HttpGet("{idModule}/lecturas")] // <-- Nombre del parámetro corregido (idModule)
        public async Task<ActionResult<IEnumerable<LecturaModuloDTO>>> GetLecturas(int idModule)
        {
            var lecturas = await _context.LecturaModulo
                .Where(l => l.Id_Modulo == idModule)
                .OrderByDescending(l => l.Date)
                .Select(l => new LecturaModuloDTO
                {
                    Id = l.Id,
                    Temperatura = l.Temperatura,
                    Humedad = l.Humedad,
                    NivelLux = l.NivelLux,
                    Date = l.Date
                })
                .ToListAsync();

            if (!lecturas.Any()) return NotFound("No hay lecturas registradas");
            return lecturas;
        }

        // GET: api/Modulos/{idModule}/lecturas/{idLectura}
        [HttpGet("{idModule}/lecturas/{idLectura}")] // <-- Parámetros corregidos
        public async Task<ActionResult<LecturaModuloDTO>> GetLectura(int idModule, int idLectura)
        {
            var lectura = await _context.LecturaModulo
                .Where(l => l.Id_Modulo == idModule && l.Id == idLectura)
                .Select(l => new LecturaModuloDTO
                {
                    Id = l.Id,
                    Temperatura = l.Temperatura,
                    Humedad = l.Humedad,
                    NivelLux = l.NivelLux,
                    Date = l.Date
                })
                .FirstOrDefaultAsync();

            if (lectura == null) return NotFound();
            return lectura;
        }

        // POST: api/Modulos/{idModule}/lecturas/random
        [HttpPost("{idModule}/lecturas/random")]
        public async Task<ActionResult<LecturaModuloDTO>> GenerateRandomReading(int idModule)
        {
            var modulo = await _context.Modulos.FindAsync(idModule);
            if (modulo == null) return NotFound("Módulo no encontrado");

            var random = new Random();
            
            var lectura = new LecturaModulo
            {
                Id_Modulo = idModule,
                Temperatura = (decimal)Math.Round(random.NextDouble() * 50 - 10, 2),
                Humedad = (decimal)Math.Round(random.NextDouble() * 80 + 20, 2),
                NivelLux = random.Next(0, 2001),
                Date = DateTime.Now
            };

            _context.LecturaModulo.Add(lectura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLectura), 
                new { idModule = idModule, idLectura = lectura.Id }, 
                new LecturaModuloDTO 
                {
                    Id = lectura.Id,
                    Temperatura = lectura.Temperatura,
                    Humedad = lectura.Humedad,
                    NivelLux = lectura.NivelLux,
                    Date = lectura.Date
                }
            );
        }

        // DTOs
        public class ModuloDTO
        {
            public int Id_Modulo { get; set; }
            public string Name { get; set; }
            public int Id_User { get; set; }
            public DateTime Date { get; set; }
            public DateTime CreatedAt { get; set; }
            public List<LecturaModuloDTO> Lecturas { get; set; }
        }

        public class CreateModuloDTO
        {
            public string Name { get; set; }
            public int Id_User { get; set; }
        }

        public class UpdateModuloDTO
        {
            public string Name { get; set; }
        }

        public class LecturaModuloDTO
        {
            public int Id { get; set; }
            public decimal Temperatura { get; set; }
            public decimal Humedad { get; set; }
            public int NivelLux { get; set; }
            public DateTime Date { get; set; }
        }

        private bool ModuloExists(int id)
        {
            return _context.Modulos.Any(e => e.Id_Modulo == id);
        }
    }
}