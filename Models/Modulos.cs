using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;




namespace RiegoWeb.Api.Models
{
    public class Modulos
    {
        public int Id_Modulos { get; set; }
        public int IdModuloIot {get; set;}
        public required string Name { get; set; }
        public required string Temperatura { get; set; }
        public required string Humedad { get; set; }
        public required string LuzNivel { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow; 


    }
}