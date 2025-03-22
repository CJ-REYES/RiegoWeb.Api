using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiegoWeb.Api.Models
{
    public class Modulos
    {
        [Key]
        public int Id_Modulo {get; set;}
        public string Name {get; set;}
        
         [ForeignKey("Id_User")]// de user
        public int Id_User{get;set;}
        public User User { get; set; }  // Propiedad de navegación
        public DateTime date{get;set;}
        public required DateTime created_at { get; set; } = DateTime.Now;
        public ICollection<LecturaModulo> Lecturas { get; set; }
    }
}