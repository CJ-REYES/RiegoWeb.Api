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
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Elimina [Required] y haz la colección nullable
    public ICollection<Modulos>? Modulos { get; set; }
}


}