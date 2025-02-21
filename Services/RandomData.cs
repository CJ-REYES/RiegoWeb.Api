using Microsoft.AspNetCore.SignalR;
using RiegoWeb.Api.Data;
using RiegoWeb.Api.Models;
using System;
using System.Collections.Generic;

public class RandomDataHub : Hub
{
    private readonly MyDbContext _context;
    private readonly Random _random = new Random();

    public RandomDataHub(MyDbContext context)
    {
        _context = context;
    }

    public List<Modulos> Generar100ModulosRandom()
    {
        var modulos = new List<Modulos>();

        for (int i = 0; i < 100; i++)
        {
            var modulo = new Modulos
            {
                Id_Modulos = _random.Next(1, 10000),  // ID aleatorio (o usa autoincremental en la BD)
                Name = $"Modulo_{_random.Next(1, 1000)}",
                Temperatura = $"{_random.Next(15, 40)}°C",
                Humedad = $"{_random.Next(30, 80)}%",
                LuzNivel = $"{_random.Next(100, 1000)} lux"
            };

            modulos.Add(modulo);
        }

        // Guardar en la BD
        _context.Modulos.AddRange(modulos);
        _context.SaveChanges(); // Persistir en la BD

        return modulos;
    }
}