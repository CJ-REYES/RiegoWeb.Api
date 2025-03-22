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
        public int Id_Historial {get ; set;}
        [ForeignKey("Id_Modulo")]// de modulos
        public int Id_Modulo {get; set;} // es la id del modelo modulo
        public Modulos Modulo { get; set; }
        public required string Temperatura { get; set; }
        public required string Humedad { get; set; }
        public required string LuzNivel { get; set; }

        public required DateTime created_at { get; set; } = DateTime.Now;

    }
}