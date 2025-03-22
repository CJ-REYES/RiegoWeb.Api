using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RiegoWeb.Api.Models;

    


namespace RiegoWeb.Api.Models
{
public class User
{
    [Key]
    public int Id_User { get; set; }
    
    [JsonPropertyName("name")]
    public string Nombre { get; set; }
    
    [JsonPropertyName("email")]
    public string Correo { get; set; }
    
    [JsonPropertyName("password")]
    public string Contraseña { get; set; }
    public  required DateTime created_at{ get;set;} = DateTime.Now;

    public ICollection<Modulos> Modulos { get; set; }

}


}