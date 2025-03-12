using Microsoft.AspNetCore.SignalR;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiegoWeb.Api.Hubs
{
    public class RandomDataHub : Hub
    {
        private readonly MyDbContext _context;
        private readonly Random _random = new Random();

        public RandomDataHub(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Modulos>> Generar100ModulosRandom()
        {
            var modulos = new List<Modulos>();

            for (int i = 0; i < 100; i++)
            {
                var modulo = new Modulos
                {
                    IdModuloIot = _random.Next(1, 1000),
                    Name = $"Modulo_{_random.Next(1, 1000)}",
                    Temperatura = $"{_random.Next(15, 40)}°C",
                    Humedad = $"{_random.Next(30, 80)}%",
                    LuzNivel = $"{_random.Next(100, 1000)} lux"
                };

                modulos.Add(modulo);
            }

            try
            {
                await _context.Modulos.AddRangeAsync(modulos);
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync("ReceiveRandomData", modulos);

                return modulos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar datos: {ex.Message}");
                throw;
            }
        }
    }
}
