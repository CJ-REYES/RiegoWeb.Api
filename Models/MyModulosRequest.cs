namespace RiegoWeb.Api.Models
{
    public class MyModulosRequest
    {
        public required string Name { get; set; }
        public int Id_User { get; set; }   // Solo el identificador del usuario
        public int IdMyModulo { get; set; } // Solo el identificador del módulo

    }
}