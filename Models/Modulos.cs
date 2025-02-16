using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiegoWeb.Api.Models
{
    public class Modulos
    {
        public int Id_Modulos { get; set; }

        public required string Temperatura { get; set; }
        public required string Humedad { get; set; }
        public required string LuzNivel { get; set; }

    }
}