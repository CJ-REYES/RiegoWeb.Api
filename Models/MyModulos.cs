using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;



namespace RiegoWeb.Api.Models
{
    public class MyModulos
    {
    public int IdMyModulo { get; set; }

    public int Id_User { get; set; }
   
    public required User User { get; set; }
   
    public int Id_Modulo { get; set; }
    
    public required Modulos Modulo { get; set; }

    }
}