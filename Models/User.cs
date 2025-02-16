using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RiegoWeb.Api.Models{

public class User
{
    public int Id_User { get; set; }
    public required string Name { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
 
    //public ICollection<MyModulo> MyModulo { get; set; }

}
}