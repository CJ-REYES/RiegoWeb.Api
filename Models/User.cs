using System.Collections.Generic;
using System.Text.Json.Serialization;
using RiegoWeb.Api.Models;

    


namespace RiegoWeb.Api.Models
{
    public class User
    {
        public int Id_User { get; set; }
        
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

       
        
    }
}