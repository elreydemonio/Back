using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NgZorroBack.Models
{
    public partial class EstadoServicio
    {
        public EstadoServicio()
        {
            Servicios = new HashSet<Servicio>();
        }
        [Key]
        public int IdEstadoServicio { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}
