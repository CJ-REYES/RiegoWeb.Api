using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;

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

        // Obtener todos los módulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modulos>>> GetModulos()
        {
            return await _context.Modulos.Include(m => m.Lecturas).ToListAsync();
        }

        // Obtener un módulo por ID
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

        // DTO para Modulos (Create/Update)
        public class ModuloDTO
{
    public int Id_Modulo { get; set; }
    public string Name { get; set; }
    public int Id_User { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<LecturaModuloDTO> Lecturas { get; set; }
}
public class LecturaModuloDTO
{
    public int Id { get; set; }
    public decimal Temperatura { get; set; }
    public decimal Humedad { get; set; }
    public int NivelLux { get; set; }
    public DateTime Date { get; set; }
}

// Modifica el método POST en el controlador:
[HttpPost]
public async Task<ActionResult<Modulos>> CreateModulo(ModuloDTO moduloDTO)
{
    var modulo = new Modulos
    {
        Name = moduloDTO.Name,
        Id_User = moduloDTO.Id_User,
        Date = moduloDTO.Date,
        CreatedAt = DateTime.Now
    };

    _context.Modulos.Add(modulo);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetModulo), new { id = modulo.Id_Modulo }, modulo);
}

        // Actualizar módulo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModulo(int id, Modulos modulo)
        {
            if (id != modulo.Id_Modulo)
            {
                return BadRequest();
            }

            modulo.CreatedAt = _context.Entry(modulo).Property(x => x.CreatedAt).OriginalValue;
            _context.Entry(modulo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuloExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Eliminar módulo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModulo(int id)
        {
            var modulo = await _context.Modulos.FindAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }

            _context.Modulos.Remove(modulo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Endpoint para recibir datos del IoT
        [HttpGet("{idModulo}/lecturas")]
public async Task<ActionResult<IEnumerable<LecturaModuloDTO>>> GetLecturas(int idModulo)
{
    var lecturas = await _context.LecturaModulo
        .Where(l => l.Id_Modulo == idModulo)
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

        // Obtener una lectura específica
        [HttpGet("{idModulo}/lecturas/{idLectura}")]
        public async Task<ActionResult<LecturaModulo>> GetLectura(int idModulo, int idLectura)
        {
            var lectura = await _context.LecturaModulo
                .FirstOrDefaultAsync(l => l.Id == idLectura && l.Id_Modulo == idModulo);

            if (lectura == null)
            {
                return NotFound();
            }

            return lectura;
        }

        private bool ModuloExists(int id)
        {
            return _context.Modulos.Any(e => e.Id_Modulo == id);
        }
         [HttpPost("{idModulo}/lecturas/random")]
        public async Task<ActionResult<LecturaModulo>> GenerateRandomReading(int idModulo)
        {
            var modulo = await _context.Modulos.FindAsync(idModulo);
            if (modulo == null) return NotFound("Módulo no encontrado");

            var random = new Random();
            
            var lectura = new LecturaModulo
            {
                Id_Modulo = idModulo,
                Temperatura = (Decimal)Math.Round(random.NextDouble() * 50 - 10, 2),  // Rango: -10°C a 40°C
                Humedad = (Decimal)Math.Round(random.NextDouble() * 80 + 20, 2),     // Rango: 20% a 100%
                NivelLux = random.Next(0, 2001),                            // Rango: 0 a 2000 lux
                Date = DateTime.Now
            };

            _context.LecturaModulo.Add(lectura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLectura), 
                new { idModulo = idModulo, idLectura = lectura.Id }, 
                lectura
            );
    }
    }
}