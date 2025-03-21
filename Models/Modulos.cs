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
        public int id {get; set;}
        public string Name {get; set;}
        
         [ForeignKey("Id_User")]// de user

         public int Id_User{get;set;}

        public DataType date{get;set;}
        public required DateTime created_at { get; set; } = DateTime.Now;

    }
}