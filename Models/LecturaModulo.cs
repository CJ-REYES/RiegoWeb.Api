using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiegoWeb.Api.Models
{
    public class LecturaModulo
    {
        [Key]
        public int id {get ; set;}
        [ForeignKey("Id")]// de modulos
        public int id_Modulo {get; set;} // es la id del modelo modulo
        public required string Temperatura { get; set; }
        public required string Humedad { get; set; }
        public required string LuzNivel { get; set; }

        public required DateTime created_at { get; set; } = DateTime.Now;

    }
}