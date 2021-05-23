using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NgZorroBack.Models
{
    public partial class InfoConductore
    {
        public InfoConductore()
        {
            Servicios = new HashSet<Servicio>();
        }
        [Key]
        public int IdInfo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string FotoConductor { get; set; }
        public int? IdConductor { get; set; }
        public int CodigoV { get; set; }

        public virtual Vehiculo CodigoVNavigation { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}
