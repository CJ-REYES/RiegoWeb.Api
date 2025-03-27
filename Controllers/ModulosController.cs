using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;
using System.ComponentModel.DataAnnotations;
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
                    Name = m.Name ?? string.Empty,
                    Id_User = m.Id_User,
                    Date = m.Date,
                    CreatedAt = m.CreatedAt,
                    Lecturas = m.Lecturas.Select(l => new LecturaModuloDTO
                    {
                        Id = l.Id,
                        Temperatura = l.Temperatura,
                        Humedad = l.Humedad,
                        NivelLux = l.NivelLux,
                        Date = l.Date
                    }).ToList()
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
                Name = modulo.Name ?? string.Empty,
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

       [HttpPost]
public async Task<ActionResult<Modulos>> CreateModulo(CreateModuloDTO moduloDTO)
{
    // Validar si se proporciona un Id_User
    if (moduloDTO.Id_User.HasValue)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == moduloDTO.Id_User.Value);
        if (!userExists)
        {
            return BadRequest("El usuario especificado no existe");
        }
    }

    var modulo = new Modulos
    {
        Name = moduloDTO.Name ?? string.Empty,
        Id_User = moduloDTO.Id_User, // Sin cast
        Date = DateTime.Now,
        CreatedAt = DateTime.Now
    };

    _context.Modulos.Add(modulo);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetModulo), new { id = modulo.Id_Modulo }, modulo);
}

        // PUT: api/Modulos/5 (Actualiza solo el nombre)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModuloName(int id, UpdateModuloNameDTO updateDto)
        {
            var modulo = await _context.Modulos.FindAsync(id);
            if (modulo == null) return NotFound();

            modulo.Name = updateDto.Name ?? modulo.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Modulos/5/user (Actualiza nombre y asigna usuario)
        [HttpPut("{id}/user")]
        public async Task<IActionResult> UpdateModuloWithUser(int id, UpdateModuloWithUserDTO updateDto)
        {
            var modulo = await _context.Modulos.FindAsync(id);
            if (modulo == null) return NotFound();

            modulo.Name = updateDto.Name ?? modulo.Name;
            modulo.Id_User = updateDto.Id_User;
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
        [HttpGet("{idModule}/lecturas")]
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
        [HttpGet("{idModule}/lecturas/{idLectura}")]
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
        
// POST: api/Modulos/{idModule}/lecturas

[HttpPost("/lecturas")]
public async Task<ActionResult<LecturaModuloDTO>> CreateLecturaModulo( CreateLecturaModuloDTO lecturaDTO)
{
     // Verificar si el módulo existe
    var modulo = await _context.Modulos.FindAsync(lecturaDTO.Id_Modulo);
    if (modulo == null)
    {
        return NotFound($"No se encontró el módulo con ID {lecturaDTO.Id_Modulo}");
    }
    // Validar los datos de entrada
    if (lecturaDTO == null)
    {
        return BadRequest("Los datos de la lectura no pueden ser nulos");
    }

    // Crear la nueva lectura
    var lectura = new LecturaModulo
    {
        Id_Modulo = lecturaDTO.Id_Modulo, // Se usa el parámetro recibido en la URL
        Temperatura = lecturaDTO.Temperatura,
        Humedad = lecturaDTO.Humedad,
        NivelLux = lecturaDTO.NivelLux,
        Date = lecturaDTO.Date ?? DateTime.Now
    };

    // Guardar en la base de datos
    _context.LecturaModulo.Add(lectura);
    await _context.SaveChangesAsync();

    // Retornar la lectura creada
    return CreatedAtAction(
    nameof(GetLectura),
    new { idModule = lectura.Id_Modulo, idLectura = lectura.Id },
    new LecturaModuloDTO
    {
        Id = lectura.Id,
        Temperatura = lectura.Temperatura,
        Humedad = lectura.Humedad,
        NivelLux = lectura.NivelLux,
        Date = lectura.Date
    });

}


// DTO para crear lecturas
public class CreateLecturaModuloDTO
{
     public int Id_Modulo { get; set; }
    [Required]
    public decimal Temperatura { get; set; }
    
    [Required]
    public decimal Humedad { get; set; }
    
    [Required]
    public int NivelLux { get; set; }
    
    public DateTime? Date { get; set; } // Opcional, si no se envía se usa la fecha actual
}

        // DTOs
        public class ModuloDTO
        {
            public int Id_Modulo { get; set; }
            public string Name { get; set; } = string.Empty;
            public int? Id_User { get; set; }
            public DateTime Date { get; set; }
            public DateTime CreatedAt { get; set; }
            public List<LecturaModuloDTO> Lecturas { get; set; } = new List<LecturaModuloDTO>();
        }

        public class CreateModuloDTO
        {
            public string Name { get; set; } = string.Empty;
            public int? Id_User { get; set; }
        }

        public class UpdateModuloNameDTO
        {
            public string Name { get; set; } = string.Empty;
        }

        public class UpdateModuloWithUserDTO
        {
            public string Name { get; set; } = string.Empty;
            public int Id_User { get; set; }
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