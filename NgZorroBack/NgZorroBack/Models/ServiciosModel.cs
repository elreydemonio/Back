using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models
{
    public class ServiciosModel
    {
        public int IdServicio { get; set; }
        public string IdCliente { get; set; }
        public int IdTipoCarga { get; set; }
        public string DireccionCarga { get; set; }
        public string DireccionEntrega { get; set; }
        public string PersonaRecibe { get; set; }
        public int CelularRecibe { get; set; }
        public int? IdConductor { get; set; }
        public decimal PrecioServicio { get; set; }
        public string FechaFin { get; set; }
        public int IdEstadoServicio { get; set; }
    }
}
