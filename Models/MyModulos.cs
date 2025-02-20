using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiegoWeb.Api.Models
{
    public class MyModulos
    {
        public int IdMyModulo { get; set; } // Clave primaria

        public required string Name { get; set; }  // Propiedad de navegación (opcional, solo si es necesario para obtener datos relacionados)

        [Required]
        public int Id_User { get; set; }   
        // Clave foránea hacia User (sólo el identificador del usuario)

        [ForeignKey("Id_User")]
        public User User { get; set; }      // Propiedad de navegación (opcional, solo si es necesario para obtener datos relacionados)

        [Required]
        public int Id_Modulo { get; set; }
        // Clave foránea hacia Modulo (sólo el identificador del módulo)

        [ForeignKey("Id_Modulo")]
        public Modulos Modulo { get; set; } 
        
         
    }
}
