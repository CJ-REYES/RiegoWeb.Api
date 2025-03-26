using System;
using System.Linq;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;

public class ModuloSeeder
{
    private readonly MyDbContext _context;

    public ModuloSeeder(MyDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!_context.Modulos.Any())
        {
            var startDate = new DateTime(2023, 10, 10);
            
            for (int i = 1; i <= 100; i++)
            {
                var modulo = new Modulos
                {
                    Name = $"Módulo {i}",
                    Id_User = null, // Todos los módulos tendrán User null
                    Date = startDate.AddDays(i - 100),
                    CreatedAt = startDate.AddDays(i - 100)
                };

                _context.Modulos.Add(modulo);
            }

            _context.SaveChanges();
        }
    }
}