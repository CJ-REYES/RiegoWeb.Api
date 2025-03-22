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
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{ get; set; }

   
    [ForeignKey("Id_Module")]
    public int Id_Modulo { get; set; } 

    
    public DateTime Date { get; set; } 

    
    public decimal Temperatura { get; set; } 
    public decimal Humedad { get; set; }
    public int NivelLux { get; set; }

    public Modulos Modulo { get; set; }
}
}