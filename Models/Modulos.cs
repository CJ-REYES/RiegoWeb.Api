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
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id_Modulo { get; set; }

    public string Name { get; set; }
    
    [ForeignKey("Id_User")]
    public int Id_User { get; set; }
    public User User { get; set; }

    public DateTime Date { get; set; }

   
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<LecturaModulo> Lecturas { get; set; }
}
}