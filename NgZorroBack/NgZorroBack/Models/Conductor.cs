using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models
{
    public class Conductor
    {
        public string id { get; set; }
        public string NombreUsuario { get; set; }
        public int IdRol { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdGenero { get; set; }
        public string Direccion { get; set; }
        public int NumeroDocumento { get; set; }
        public int Celular { get; set; }
        public int IdInfo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string FotoConductor { get; set; }
        public string IdConductor { get; set; }
        public int CodigoV { get; set; }
    }
}
