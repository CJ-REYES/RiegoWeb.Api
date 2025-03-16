namespace RiegoWeb.Api.Models
{
    public class HistoriaDeModulos
    {
        public int Id_HistoriaDeModulos { get; set; }

        public int Id_Modulos { get; set; }
        public Modulos Modulos { get; set; }
        public required string Name { get; set; }
        
        public required string Temperatura { get; set; }
        public required string Humedad { get; set; }
        public required string LuzNivel { get; set; }
        public required DateTime Fecha { get; set; } = DateTime.Now;


    }
}