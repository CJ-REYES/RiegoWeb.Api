using System.Collections.Generic;
using System.Text.Json.Serialization;
using RiegoWeb.Api.Models;

    


namespace RiegoWeb.Api.Models
{
public class User
{
    public int Id_User { get; set; }
    
    [JsonPropertyName("name")]
    public string Nombre { get; set; }
    
    [JsonPropertyName("email")]
    public string Correo { get; set; }
    
    [JsonPropertyName("password")]
    public string Contraseña { get; set; }
}


}