using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NgZorroBack.Models
{
    public partial class TipoCarga
    {
        public TipoCarga()
        {
            Servicios = new HashSet<Servicio>();
        }
        [Key]
        public int IdTipoCarga { get; set; }
        public string DescripcionCarga { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}
