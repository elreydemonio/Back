using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NgZorroBack.Models
{
    public partial class TipoVehiculo
    {
        public TipoVehiculo()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }
        [Key]
        public int IdTipoVehiculo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
