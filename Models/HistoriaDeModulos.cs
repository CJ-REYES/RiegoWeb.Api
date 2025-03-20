using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


namespace RiegoWeb.Api.Models
{
    public class HistoriaDeModulos
    {
        public int Id_HistoriaDeModulos { get; set; }

        public int Id_Modulos { get; set; }
         [ForeignKey("Id_Modulos")]
        public Modulos Modulo { get; set; }
        public required string Name { get; set; }
        
        public required string Temperatura { get; set; }
        public required string Humedad { get; set; }
        public required string LuzNivel { get; set; }
        public required DateTime Fecha { get; set; } = DateTime.Now;


    }
}